using System;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentValidationData
    {
        public string PaymentRequestId { get; set; }

        public Guid PartnerId { get; set; }
    }
}
