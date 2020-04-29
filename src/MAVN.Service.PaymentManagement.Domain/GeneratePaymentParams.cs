using System;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class GeneratePaymentData
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public Guid PartnerId { get; set; }

        public Guid CustomerId { get; set; }
    }
}
