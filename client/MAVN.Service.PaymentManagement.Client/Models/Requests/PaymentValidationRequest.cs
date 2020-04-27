using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Requests
{
    /// <summary>
    /// Payment validation request model
    /// </summary>
    [PublicAPI]
    public class PaymentValidationRequest
    {
        /// <summary>Payment request id</summary>
        public string PaymentRequestId { get; set; }

        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }
    }
}
