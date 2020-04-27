using System;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Requests
{
    /// <summary>
    /// Payment generation request model
    /// </summary>
    [PublicAPI]
    public class PaymentGenerationRequest
    {
        /// <summary>Amount</summary>
        public decimal Amount { get; set; }

        /// <summary>Currnecy</summary>
        public string Currency { get; set; }

        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }

        /// <summary>Customer id</summary>
        public Guid CustomerId { get; set; }
    }
}
