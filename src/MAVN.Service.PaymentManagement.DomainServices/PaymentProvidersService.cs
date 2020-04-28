using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.PaymentIntegrationPlugin.Client;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Contract;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.Domain.Repositories;
using MAVN.Service.PaymentManagement.Domain.Services;
using StackExchange.Redis;

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

        public PaymentProvidersService(
            IPaymentRequestsRepository paymentRequestsRepository,
            IRabbitPublisher<PaymentCompletedEvent> rabbitPublisher,
            IConnectionMultiplexer connectionMultiplexer,
            IEnumerable<(string name, string url)> integrationPlugins,
            string defaultProvider,
            string successUrlTemplate,
            string failUrlTemplate,
            IMapper mapper)
        {
            _paymentRequestsRepository = paymentRequestsRepository;
            _rabbitPublisher = rabbitPublisher;
            _db = connectionMultiplexer.GetDatabase();
            _pluginsDict = integrationPlugins.ToDictionary(i => i.name, i => i.url);
            _defaultProvider = defaultProvider;
            _successUrlTemplate = successUrlTemplate;
            _failUrlTemplate = failUrlTemplate;
            _mapper = mapper;
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
                        PaymentProvider = pair.Key,
                        Requirements = _mapper.Map<List<PaymentProviderRequirement>>(pluginRequirements.Properties),
                    });
            }

            return result;
        }

        public async Task<List<PaymentProviderSupportedCurrencies>> GetSupportedCurrenciesAsync(string paymentProvider, Guid? partnerId)
        {
            var result = new List<PaymentProviderSupportedCurrencies>();
            var pluginUrls = new List<string>();
            if (!string.IsNullOrWhiteSpace(paymentProvider) && _pluginsDict.TryGetValue(paymentProvider, out var url))
                pluginUrls.Add(url);
            else if (partnerId.HasValue)
                pluginUrls.Add(ResolvePaymentProviderClientUrl(partnerId.Value));
            else
                pluginUrls.AddRange(_pluginsDict.Values);
            foreach (var pluginUrl in pluginUrls)
            {
                var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);
                var supportedCurrencies = await pluginClient.Api.GetPaymentIntegrationSupportedCurrenciesAsync();
                result.Add(new PaymentProviderSupportedCurrencies
                {
                    PaymentProvider = paymentProvider,
                    SupportedCurrencies = supportedCurrencies,
                });
            }

            return result;
        }

        public Task<bool> CheckPaymentIntegrationAsync(Guid partnerId)
        {
            var pluginUrl = ResolvePaymentProviderClientUrl(partnerId);
            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);
            return pluginClient.Api.CheckPaymentIntegrationAsync(
                new CheckPaymentIntegrationRequest { PartnerId  = partnerId });
        }

        public async Task<PaymentGenerationResult> GeneratePaymentAsync(GeneratePaymentData data)
        {
            var pluginUrl = ResolvePaymentProviderClientUrl(data.PartnerId);
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

            var now = DateTime.UtcNow;
            var paymentRequest = new PaymentRequest
            {
                Id = paymentRequestId,
                CustomerId = data.CustomerId,
                PartnerId = data.PartnerId,
                Amount = data.Amount,
                Currency = data.Currency,
                PaymentId = result.PaymentId,
                PaymentStatus = PaymentStatus.Pending.ToString(),
                CreatedAt = now,
                ModifiedAt = now,
            };
            await _paymentRequestsRepository.CreateAsync(paymentRequest);

            return new PaymentGenerationResult
            {
                PaymentRequestId = paymentRequestIdStr,
                PaymentPageUrl = result.PaymentPageUrl,
            };
        }

        public async Task ValidatePaymentAsync(PaymentValidationData data)
        {
            var pluginUrl = ResolvePaymentProviderClientUrl(data.PartnerId);
            var pluginClient = new PaymentIntegrationPluginClient(pluginUrl);
            var paymentRequest = await _paymentRequestsRepository.GetById(data.PaymentRequestId);
            if (paymentRequest == null)
                return;

            var paymentStatus = await pluginClient.Api.CheckPaymentAsync(
                new CheckPaymentRequest
                {
                    PartnerId = data.PartnerId,
                    PaymentId = paymentRequest.PaymentId,
                });
            var currentStatus = paymentStatus.ToString();
            if (paymentRequest.PaymentStatus == currentStatus)
                return;

            var partnerIdStr = data.PartnerId.ToString();
            for (int i = 0; i < MaxAttemptsCount; ++i)
            {
                var locked = await _db.LockTakeAsync(data.PaymentRequestId, partnerIdStr, _lockTimeout);
                if (locked)
                {
                    await Task.Delay(_lockTimeout);
                    continue;
                }

                var now = DateTime.UtcNow;
                paymentRequest.PaymentStatus = currentStatus;
                paymentRequest.ModifiedAt = now;
                await _paymentRequestsRepository.UpdateAsync(paymentRequest);

                await _db.LockReleaseAsync(data.PaymentRequestId, partnerIdStr);

                if (paymentStatus != PaymentStatus.Success)
                    return;

                var evt = new PaymentCompletedEvent
                {
                    PaymentRequestId = data.PaymentRequestId,
                    CustomerId = paymentRequest.CustomerId,
                    PartnerId = paymentRequest.PartnerId,
                    Amount = paymentRequest.Amount,
                    Currency = paymentRequest.Currency,
                    Timestamp = now,
                };
                await _rabbitPublisher.PublishAsync(evt);

                return;
            }

            throw new InvalidOperationException($"Can't lock for payment request {data.PaymentRequestId}");
        }

        private string ResolvePaymentProviderClientUrl(Guid partnerId)
        {
            if (_pluginsDict.TryGetValue(_defaultProvider, out var url))
                return url;

            return _pluginsDict.First().Value;
        }
    }
}
