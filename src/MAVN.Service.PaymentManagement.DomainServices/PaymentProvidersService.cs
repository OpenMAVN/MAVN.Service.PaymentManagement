using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.PaymentIntegrationPlugin.Client;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Contract;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.Domain.Enums;
using MAVN.Service.PaymentManagement.Domain.Repositories;
using MAVN.Service.PaymentManagement.Domain.Services;
using StackExchange.Redis;
using PaymentStatus = MAVN.Service.PaymentManagement.Domain.Enums.PaymentStatus;

namespace MAVN.Service.PaymentManagement.DomainServices
{
    public class PaymentProvidersService : IPaymentProvidersService
    {
        private const int MaxAttemptsCount = 5;

        private readonly IPaymentRequestsRepository _paymentRequestsRepository;
        private readonly IRabbitPublisher<PaymentCompletedEvent> _rabbitPublisher;
        private readonly IDatabase _db;
        private readonly TimeSpan _lockTimeout = TimeSpan.FromMinutes(1);
        private readonly string _defaultProvider;
        private readonly string _successUrlTemplate;
        private readonly string _failUrlTemplate;
        private readonly Dictionary<string, string> _pluginsDict;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public PaymentProvidersService(
            IPaymentRequestsRepository paymentRequestsRepository,
            IRabbitPublisher<PaymentCompletedEvent> rabbitPublisher,
            IConnectionMultiplexer connectionMultiplexer,
            IEnumerable<(string name, string url)> integrationPlugins,
            string defaultProvider,
            string successUrlTemplate,
            string failUrlTemplate,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _paymentRequestsRepository = paymentRequestsRepository;
            _rabbitPublisher = rabbitPublisher;
            _db = connectionMultiplexer.GetDatabase();
            _pluginsDict = integrationPlugins.ToDictionary(i => i.name, i => i.url);
            _defaultProvider = defaultProvider;
            _successUrlTemplate = successUrlTemplate;
            _failUrlTemplate = failUrlTemplate;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        public async Task<List<PaymentProviderRequirements>> GetPaymentProvidersRequirementsAsync()
        {
            var result = new List<PaymentProviderRequirements>();

            foreach (var pair in _pluginsDict)
            {
                var paymentProviderIntegrationPluginClient = new PaymentIntegrationPluginClient(pair.Value);
                var pluginRequirements = await paymentProviderIntegrationPluginClient.Api.GetPaymentIntegrationPropertiesAsync();
                result.Add(
                    new PaymentProviderRequirements
                    {
                        PaymentProvider = pluginRequirements.PaymentProvider,
                        Requirements = _mapper.Map<List<PaymentProviderRequirement>>(pluginRequirements.Properties),
                    });
            }

            return result;
        }

        public async Task<List<PaymentProviderSupportedCurrencies>> GetSupportedCurrenciesAsync(Guid? partnerId)
        {
            var result = new List<PaymentProviderSupportedCurrencies>();
            var pluginProvidersAndUrls = new List<(string provider, string url)>();

            if (partnerId.HasValue)
                pluginProvidersAndUrls.Add(ResolvePaymentProviderClientUrlByPartner(partnerId.Value));
            else
                pluginProvidersAndUrls.AddRange(_pluginsDict.Select(x => (x.Key, x.Value)));

            foreach (var pluginProviderAndUrl in pluginProvidersAndUrls)
            {
                var pluginClient = new PaymentIntegrationPluginClient(pluginProviderAndUrl.url);
                var supportedCurrencies = await pluginClient.Api.GetPaymentIntegrationSupportedCurrenciesAsync();
                result.Add(new PaymentProviderSupportedCurrencies
                {
                    PaymentProvider = pluginProviderAndUrl.provider,
                    SupportedCurrencies = supportedCurrencies,
                });
            }

            return result;
        }

        public async Task<PaymentIntegrationCheckErrorCodes> CheckPaymentIntegrationAsync(Guid partnerId, string paymentIntegrationProperties, string paymentIntegrationProvider)
        {
            var pluginUrl = string.IsNullOrEmpty(paymentIntegrationProvider)
                ? ResolvePaymentProviderClientUrlByPartner(partnerId).url
                : ResolvePaymentProviderClientUrlByName(paymentIntegrationProvider);

            if (string.IsNullOrEmpty(pluginUrl))
                return PaymentIntegrationCheckErrorCodes.PaymentIntegrationProviderIsMissing;

            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);
            var errorCode = await pluginClient.Api.CheckPaymentIntegrationAsync(
                new CheckPaymentIntegrationRequest { PartnerId = partnerId, PaymentIntegrationProperties = paymentIntegrationProperties });
            return _mapper.Map<PaymentIntegrationCheckErrorCodes>(errorCode);
        }

        public async Task<PaymentGenerationResult> GeneratePaymentAsync(GeneratePaymentData data)
        {
            var (provider, pluginUrl) = ResolvePaymentProviderClientUrlByPartner(data.PartnerId);
            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);
            var paymentRequestId = Guid.NewGuid();
            var paymentRequestIdStr = paymentRequestId.ToString();
            var result = await pluginClient.Api.GeneratePaymentAsync(
                new GeneratePaymentRequest
                {
                    PaymentRequestId = paymentRequestIdStr,
                    PartnerId = data.PartnerId,
                    Amount = data.Amount,
                    Currency = data.Currency,
                    SuccessRedirectUrl = string.Format(_successUrlTemplate, paymentRequestIdStr),
                    FailRedirectUrl = string.Format(_failUrlTemplate, paymentRequestIdStr),

                });
            if (result.ErrorCode != CheckIntegrationErrorCode.None)
            {
                _log.Warning($"Received {result.ErrorCode} error code from plugin on payment generation");
                return new PaymentGenerationResult
                {
                    ErrorCode = GeneratePaymentErrorCode.InvalidPartnerConfiguration,
                };
            }

            var now = DateTime.UtcNow;
            var paymentRequest = new PaymentRequest
            {
                Id = paymentRequestId,
                CustomerId = data.CustomerId,
                PartnerId = data.PartnerId,
                Amount = data.Amount,
                Currency = data.Currency,
                PaymentId = result.PaymentId,
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = now,
                ModifiedAt = now,
                ExternalPaymentEntityId = data.ExternalPaymentEntityId,
            };
            await _paymentRequestsRepository.CreateAsync(paymentRequest);

            return new PaymentGenerationResult
            {
                ErrorCode = GeneratePaymentErrorCode.None,
                PaymentRequestId = paymentRequestId,
                PaymentPageUrl = result.PaymentPageUrl,
            };
        }

        public async Task<string> ValidatePaymentAsync(PaymentValidationData data)
        {
            var paymentRequest = await _paymentRequestsRepository.GetById(data.PaymentRequestId);

            if (paymentRequest == null)
                return null;

            if (paymentRequest.PaymentStatus == PaymentStatus.Success)
            {
                _log.Warning($"Payment marked as successful ({paymentRequest.PaymentStatus}) on {paymentRequest.ModifiedAt.ToJson()}");
                return paymentRequest.PaymentStatus.ToString();
            }

            var (provider, pluginUrl) = ResolvePaymentProviderClientUrlByPartner(paymentRequest.PartnerId);
            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);

            var paymentStatus = await pluginClient.Api.CheckPaymentAsync(
                new CheckPaymentRequest
                {
                    PartnerId = paymentRequest.PartnerId,
                    PaymentId = paymentRequest.PaymentId,
                });

            if (paymentStatus.ErrorCode != CheckIntegrationErrorCode.None)
            {
                _log.Warning($"Received an error during payment validation - {paymentStatus.ErrorCode}");
                return paymentStatus.ErrorCode.ToString();
            }

            var newStatus = (PaymentStatus)paymentStatus.PaymentStatus;

            if (paymentRequest.PaymentStatus == newStatus)
            {
                _log.Warning($"Status {newStatus} hasn't changed since {paymentRequest.ModifiedAt.ToJson()}");
                return newStatus.ToString();
            }

            var paymentRequestIdStr = data.PaymentRequestId.ToString();
            var partnerIdStr = paymentRequest.PartnerId.ToString();
            var now = DateTime.UtcNow;

            await WithRedisLockAsync(async () =>
            {
                var previousStatus = paymentRequest.PaymentStatus;
                paymentRequest.PaymentStatus = newStatus;
                paymentRequest.ModifiedAt = now;
                await _paymentRequestsRepository.UpdateAsync(paymentRequest);

                _log.Info($"Updated payment status to {newStatus} from {previousStatus}", paymentRequestIdStr);
            }, paymentRequestIdStr, partnerIdStr);

            if ((PaymentStatus)paymentStatus.PaymentStatus != PaymentStatus.Success)
                return newStatus.ToString();

            var evt = new PaymentCompletedEvent
            {
                PaymentRequestId = paymentRequestIdStr,
                CustomerId = paymentRequest.CustomerId,
                PartnerId = paymentRequest.PartnerId,
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                Timestamp = now,
            };
            await _rabbitPublisher.PublishAsync(evt);

            _log.Info("Publiched payment completed event", evt);

            return newStatus.ToString();
        }

        public async Task<string> GetPaymentUrlByExternalPaymentId(string externalPaymentId)
        {
            if (string.IsNullOrEmpty(externalPaymentId))
                return null;

            var paymentRequest = await _paymentRequestsRepository.GetByExternalId(externalPaymentId);

            if (paymentRequest == null)
                return null;

            var (provider, pluginUrl) = ResolvePaymentProviderClientUrlByPartner(paymentRequest.PartnerId);
            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);

            var paymentStatusResponse = await pluginClient.Api.CheckPaymentAsync(
                new CheckPaymentRequest
                {
                    PartnerId = paymentRequest.PartnerId,
                    PaymentId = paymentRequest.PaymentId,
                });

            return paymentStatusResponse.PaymentUrl;
        }

        public async Task<bool> CancelPaymentAsync(Guid paymentRequestId)
        {
            var paymentRequest = await _paymentRequestsRepository.GetById(paymentRequestId);

            if(paymentRequest == null)
                return false;

            var paymentRequestIdStr = paymentRequestId.ToString();
            await WithRedisLockAsync(async () =>
            {
                var now = DateTime.UtcNow;
                var previousStatus = paymentRequest.PaymentStatus;
                paymentRequest.PaymentStatus = PaymentStatus.Cancelled;
                paymentRequest.ModifiedAt = now;
                await _paymentRequestsRepository.UpdateAsync(paymentRequest);

                _log.Info($"Updated payment status to Cancelled from {previousStatus}", context: paymentRequestId.ToString());
            }, paymentRequestIdStr, paymentRequest.PartnerId.ToString());

            return true;
        }

        private async Task WithRedisLockAsync(Func<Task> action, string paymentRequestIdStr, string redisValue)
        {
            var redisKey = GetRedisKey(paymentRequestIdStr);
            for (var i = 0; i < MaxAttemptsCount; ++i)
            {
                var locked = await _db.LockTakeAsync(redisKey, redisValue, _lockTimeout);

                if (!locked)
                {
                    _log.Info("Couldn't lock for payment request", redisKey);
                    await Task.Delay(_lockTimeout.Add(TimeSpan.FromMilliseconds(100)));
                    continue;
                }

                await action();

                await _db.LockReleaseAsync(redisKey, redisValue);
                return;
            }

            throw new InvalidOperationException($"Can't lock for payment request {paymentRequestIdStr}");
        }

        private string GetRedisKey(string paymentRequestId)
        {
            return $"{nameof(PaymentManagement)}:{nameof(paymentRequestId)}:{paymentRequestId}";
        }

        private (string provider, string url) ResolvePaymentProviderClientUrlByPartner(Guid partnerId)
        {
            if (_pluginsDict.TryGetValue(_defaultProvider, out var url))
                return (_defaultProvider, url);

            var (provider, pluginUrl) = _pluginsDict.First();
            return (provider, pluginUrl);
        }

        private string ResolvePaymentProviderClientUrlByName(string paymentIntegrationProvider)
        {
            return _pluginsDict.TryGetValue(paymentIntegrationProvider, out var url) ? url : null;
        }
    }
}
