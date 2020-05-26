using System;
using MAVN.Service.PaymentManagement.Domain.Enums;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentRequest : IPaymentRequest
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string PaymentId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ExternalPaymentEntityId { get; set; }
    }
}
