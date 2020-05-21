using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PaymentManagement.Client;
using MAVN.Service.PaymentManagement.Client.Models.Requests;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PaymentManagement.Controllers
{
    [Route("api/payments")]
    public class PaymentManagementController : Controller, IPaymentManagementApi
    {
        private readonly IPaymentProvidersService _paymentProvidersService;
        private readonly IMapper _mapper;

        public PaymentManagementController(
            IPaymentProvidersService paymentProvidersService,
            IMapper mapper)
        {
            _paymentProvidersService = paymentProvidersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of payment providers requirements.
        /// </summary>
        [HttpGet("requirements")]
        [ProducesResponseType(typeof(AvailablePaymentProvidersRequirementsResponse), (int)HttpStatusCode.OK)]
        public async Task<AvailablePaymentProvidersRequirementsResponse> GetAvailablePaymentProvidersRequirementsAsync()
        {
            var requirements = await _paymentProvidersService.GetPaymentProvidersRequirementsAsync();

            return new AvailablePaymentProvidersRequirementsResponse
            {
                ProvidersRequirements = _mapper.Map<List<PaymentProviderProperties>>(requirements),
            };
        }

        /// <summary>
        /// Get a list of supported currencies
        /// </summary>
        [HttpGet("currencies")]
        [ProducesResponseType(typeof(PaymentIntegrationsSupportedCurrenciesResponse), (int)HttpStatusCode.OK)]
        public async Task<PaymentIntegrationsSupportedCurrenciesResponse> GetPaymentIntegrationsSupportedCurrenciesAsync(
            [FromQuery] PaymentIntegrationsSupportedCurrenciesRequest request)
        {
            var supportedCurrencies = await _paymentProvidersService.GetSupportedCurrenciesAsync(request.PartnerId);

            return new PaymentIntegrationsSupportedCurrenciesResponse
            {
                ProvidersSupportedCurrencies = _mapper.Map<List<PaymentIntegrationSupportedCurrencies>>(supportedCurrencies),
            };
        }

        /// <summary>
        /// Checks configuration of payment integration for partner
        /// </summary>
        /// <param name="request">Check payment integration request</param>
        [HttpPost("check")]
        [ProducesResponseType(typeof(CheckPaymentIntegrationErrorCode), (int)HttpStatusCode.OK)]
        public async Task<CheckPaymentIntegrationErrorCode> CheckPaymentIntegrationAsync([FromBody] PaymentIntegrationCheckRequest request)
        {
            var errorCode = await _paymentProvidersService.CheckPaymentIntegrationAsync(request.PartnerId, request.PaymentIntegrationProperties, request.PaymentIntegrationProvider);
            return _mapper.Map<CheckPaymentIntegrationErrorCode>(errorCode);
        }

        /// <summary>
        /// Generates a payment from integrated payment provider.
        /// </summary>
        /// <param name="request">Payment generation request</param>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentGenerationResponse), (int)HttpStatusCode.OK)]
        public async Task<PaymentGenerationResponse> GeneratePaymentAsync([FromBody] PaymentGenerationRequest request)
        {
            var requestData = _mapper.Map<GeneratePaymentData>(request);

            var result = await _paymentProvidersService.GeneratePaymentAsync(requestData);

            return _mapper.Map<PaymentGenerationResponse>(result);
        }

        /// <summary>
        /// Validates payment status
        /// </summary>
        /// <param name="request">Validate payment request</param>
        [HttpPost("validation")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public Task<string> ValidatePaymentAsync([FromBody] PaymentValidationRequest request)
        {
            var requestData = _mapper.Map<PaymentValidationData>(request);

            return _paymentProvidersService.ValidatePaymentAsync(requestData);
        }

        /// <summary>
        /// Get payment info by external id
        /// </summary>
        [HttpGet("info")]
        [ProducesResponseType(typeof(PaymentInfoResponse), (int)HttpStatusCode.OK)]
        public async Task<PaymentInfoResponse> GetPaymentInfoAsync([FromQuery] GetPaymentInfoRequest request)
        {
            var paymentUrl = await _paymentProvidersService.GetPaymentUrlByExternalPaymentId(request.ExternalPaymentEntityId);

            return new PaymentInfoResponse
            {
                PaymentUrl = paymentUrl,
            };
        }
    }
}
