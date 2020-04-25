using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests
{
    /// <summary>
    /// Generate payment request model
    /// </summary>
    [PublicAPI]
    public class GeneratePaymentRequest
    {
        /// <summary>Payment request id</summary>
        public string PaymentRequestId { get; set; }

        /// <summary>Amount</summary>
        public decimal Amount { get; set; }

        /// <summary>Currnecy</summary>
        public string Currency { get; set; }

        /// <summary>SuccessRedirectUrl</summary>
        public string SuccessRedirectUrl { get; set; }

        /// <summary>SuccessRedirectUrl</summary>
        public string FailRedirectUrl { get; set; }

        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }
    }
}
