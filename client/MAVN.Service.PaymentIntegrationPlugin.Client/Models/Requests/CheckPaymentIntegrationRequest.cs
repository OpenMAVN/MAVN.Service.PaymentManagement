using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests
{
    /// <summary>
    /// Check payment integration request model
    /// </summary>
    [PublicAPI]
    public class CheckPaymentIntegrationRequest
    {
        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }

        /// <summary>
        /// Payment integration properties
        /// </summary>
        public string PaymentIntegrationProperties { get; set; }
    }
}
