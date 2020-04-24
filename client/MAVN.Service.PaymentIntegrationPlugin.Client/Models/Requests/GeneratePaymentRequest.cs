using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests
{
    /// <summary>
    /// Generate payment request model
    /// </summary>
    [PublicAPI]
    public class GeneratePaymentRequest
    {
        /// <summary>Amount</summary>
        public decimal Amount { get; set; }

        /// <summary>Currnecy</summary>
        public string Currency { get; set; }

        /// <summary>SuccessRedirectUrlBase</summary>
        public string SuccessRedirectUrlBase { get; set; }

        /// <summary>SuccessRedirectUrlBase</summary>
        public string FailRedirectUrlBase { get; set; }
    }
}
