using FluentValidation;
using MAVN.Service.PaymentManagement.Client.Models.Requests;

namespace MAVN.Service.PaymentManagement.Validation
{
    public class PaymentIntegrationCheckRequestValidator : AbstractValidator<PaymentIntegrationCheckRequest>
    {
        public PaymentIntegrationCheckRequestValidator()
        {
            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
