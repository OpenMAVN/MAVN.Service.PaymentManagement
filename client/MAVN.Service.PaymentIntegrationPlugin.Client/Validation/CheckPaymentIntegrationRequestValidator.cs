using FluentValidation;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Requests;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Validation
{
    /// <summary>
    /// CheckPaymentIntegrationRequest model validator.
    /// </summary>
    public class CheckPaymentIntegrationRequestValidator : AbstractValidator<CheckPaymentIntegrationRequest>
    {
        /// <summary>
        /// C-tor
        /// </summary>
        public CheckPaymentIntegrationRequestValidator()
        {
            RuleFor(x => x.PartnerId)
                .Must(x => x != default)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
