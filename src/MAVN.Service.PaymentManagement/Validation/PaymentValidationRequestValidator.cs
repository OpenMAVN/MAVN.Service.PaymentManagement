using FluentValidation;
using MAVN.Service.PaymentManagement.Client.Models.Requests;

namespace MAVN.Service.PaymentManagement.Validation
{
    public class PaymentValidationRequestValidator : AbstractValidator<PaymentValidationRequest>
    {
        public PaymentValidationRequestValidator()
        {
            RuleFor(x => x.PaymentRequestId)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.PaymentRequestId)} required");

            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
