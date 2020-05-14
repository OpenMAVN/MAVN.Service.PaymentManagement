using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Requests
{
    /// <summary>
    /// Payment integration check request model.
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationCheckRequest
    {
        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }

        /// <summary>
        /// Payment integration properties
        /// </summary>
        public string PaymentIntegrationProperties { get; set; }

        /// <summary>
        /// Payment integration provider
        /// </summary>
        public string PaymentIntegrationProvider { get; set; }
    }
}
