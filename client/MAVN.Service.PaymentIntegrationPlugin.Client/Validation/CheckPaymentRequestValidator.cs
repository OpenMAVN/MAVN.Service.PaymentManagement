using FluentValidation;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Validation
{
    /// <summary>
    /// CheckPaymentRequest model validator
    /// </summary>
    public class CheckPaymentRequestValidator : AbstractValidator<CheckPaymentRequest>
    {
        /// <summary>
        /// C-tor
        /// </summary>
        public CheckPaymentRequestValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty()
                .WithMessage(x => $"{nameof(x.PaymentId)} required");

            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
