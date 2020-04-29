using FluentValidation;
using MAVN.Service.PaymentManagement.Client.Models.Requests;

namespace MAVN.Service.PaymentManagement.Validation
{
    public class PaymentGenerationRequestValidator : AbstractValidator<PaymentGenerationRequest>
    {
        public PaymentGenerationRequestValidator()
        {
            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");

            RuleFor(x => x.Amount)
                .Must(x => x >= 0)
                .WithMessage(x => $"{nameof(x.Amount)} must be a positive number");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.Currency)} required");

            RuleFor(x => x.CustomerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.CustomerId)} required");
        }
    }
}
