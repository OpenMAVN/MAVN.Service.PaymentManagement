using System;

namespace MAVN.Service.PaymentManagement.Contract
{
    /// <summary>
    /// Payment completed event
    /// </summary>
    public class PaymentCompletedEvent
    {
        /// <summary>Timestamp</summary>
        public DateTime Timestamp { get; set; }

        /// <summary>Customer id</summary>
        public Guid CustomerId { get; set; }

        /// <summary>Partner id</summary>
        public Guid PartnerId { get; set; }

        /// <summary>Amount</summary>
        public decimal Amount { get; set; }

        /// <summary>Currency</summary>
        public string Currency { get; set; }

        /// <summary>Payment request id</summary>
        public string PaymentRequestId { get; set; }
    }
}
