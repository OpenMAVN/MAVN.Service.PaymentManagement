using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses;
using Refit;

namespace MAVN.Service.PaymentIntegrationPlugin.Client
{
    /// <summary>
    /// PaymentManagement client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPaymentIntegrationPluginApi
    {
        /// <summary>
        /// Get a list of payment integration properties
        /// </summary>
        [Get("/api/payment/requirements")]
        Task<PaymentIntegrationResponse> GetPaymentIntegrationPropertiesAsync();

        /// <summary>
        /// Get a list of supported currencies
        /// </summary>
        [Get("/api/payment/currencies")]
        Task<List<string>> GetPaymentIntegrationSupportedCurrenciesAsync();

        /// <summary>
        /// Checks configuration of payment integration for partner
        /// </summary>
        /// <param name="partnerId"></param>
        [Post("/api/payment/check")]
        Task<bool> CheckPaymentIntegrationAsync(Guid partnerId);

        /// <summary>
        /// Generates a payment from integrated payment provider.
        /// </summary>
        /// <param name="request">Payment generation request</param>
        [Post("/api/payment")]
        Task<PaymentResponse> GeneratePaymentAsync(GeneratePaymentRequest request);

        /// <summary>
        /// Checks for a payment status
        /// </summary>
        /// <param name="paymentId"></param>
        [Get("/api/payment")]
        Task<PaymentStatus> CheckPaymentAsync(string paymentId);
    }
}
