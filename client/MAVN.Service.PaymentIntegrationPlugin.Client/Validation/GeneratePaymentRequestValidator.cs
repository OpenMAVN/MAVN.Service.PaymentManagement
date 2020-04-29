using FluentValidation;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Validation
{
    /// <summary>
    /// GeneratePaymentRequest model validator
    /// </summary>
    public class GeneratePaymentRequestValidator : AbstractValidator<GeneratePaymentRequest>
    {
        /// <summary>
        /// C-tor
        /// </summary>
        public GeneratePaymentRequestValidator()
        {
            RuleFor(x => x.PaymentRequestId)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.PaymentRequestId)} required");

            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");

            RuleFor(x => x.Amount)
                .Must(x => x >= 0)
                .WithMessage(x => $"{nameof(x.Amount)} must be a positive number");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.Currency)} required");

            RuleFor(x => x.SuccessRedirectUrl)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.SuccessRedirectUrl)} required");

            RuleFor(x => x.FailRedirectUrl)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.FailRedirectUrl)} required");
        }
    }
}
