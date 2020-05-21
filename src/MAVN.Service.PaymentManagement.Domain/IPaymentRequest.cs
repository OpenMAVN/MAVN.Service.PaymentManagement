using System;

namespace MAVN.Service.PaymentManagement.Domain
{
    public interface IPaymentRequest
    {
        public Guid Id { get; set; }

        public Guid PartnerId { get; set; }

        public string PaymentId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
        public string ExternalPaymentEntityId { get; set; }
    }
}
