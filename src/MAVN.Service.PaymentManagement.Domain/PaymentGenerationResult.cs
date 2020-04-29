using System;
using MAVN.Service.PaymentManagement.Domain.Enums;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentGenerationResult
    {
        public PaymentIntegrationCkeckErrorCodes ErrorCode { get; set; }

        public string PaymentPageUrl { get; set; }

        public Guid PaymentRequestId { get; set; }
    }
}
