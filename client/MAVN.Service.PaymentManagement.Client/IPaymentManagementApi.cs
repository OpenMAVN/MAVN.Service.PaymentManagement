using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PaymentManagement.Client.Models.Requests;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using Refit;

namespace MAVN.Service.PaymentManagement.Client
{
    /// <summary>
    /// PaymentManagement client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPaymentManagementApi
    {
        /// <summary>
        /// Retrieves a list of payment providers requirements.
        /// </summary>
        [Get("/api/payments/requirements")]
        Task<AvailablePaymentProvidersRequirementsResponse> GetAvailablePaymentProvidersRequirementsAsync();

        /// <summary>
        /// Get a list of supported currencies
        /// </summary>
        [Get("/api/payments/currencies")]
        Task<PaymentIntegrationsSupportedCurrenciesResponse> GetPaymentIntegrationsSupportedCurrenciesAsync(
            [Query] PaymentIntegrationsSupportedCurrenciesRequest request);

        /// <summary>
        /// Checks configuration of payment integration for partner
        /// </summary>
        /// <param name="request">Check payment integration request</param>
        [Post("/api/payments/check")]
        Task<CheckPaymentIntegrationErrorCode> CheckPaymentIntegrationAsync([Body] PaymentIntegrationCheckRequest request);

        /// <summary>
        /// Generates a payment from integrated payment provider.
        /// </summary>
        /// <param name="request">Payment generation request</param>
        [Post("/api/payments")]
        Task<PaymentGenerationResponse> GeneratePaymentAsync([Body] PaymentGenerationRequest request);

        /// <summary>
        /// Validates payment status
        /// </summary>
        /// <param name="request">Validate payment request</param>
        [Post("/api/payments/validation")]
        Task<string> ValidatePaymentAsync([Body] PaymentValidationRequest request);
    }
}
