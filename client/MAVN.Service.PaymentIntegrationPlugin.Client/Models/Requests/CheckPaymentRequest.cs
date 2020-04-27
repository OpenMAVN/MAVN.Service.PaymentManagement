using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests
{
    /// <summary>
    /// Check payment request model
    /// </summary>
    [PublicAPI]
    public class CheckPaymentRequest
    {
        /// <summary>Payment id</summary>
        public string PaymentId { get; set; }

        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }
    }
}
