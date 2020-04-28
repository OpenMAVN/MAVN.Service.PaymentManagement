using System;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentValidationData
    {
        public Guid PaymentRequestId { get; set; }

        public Guid PartnerId { get; set; }
    }
}
