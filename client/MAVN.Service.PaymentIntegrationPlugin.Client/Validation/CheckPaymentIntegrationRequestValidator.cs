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
                .Must((model, value) =>
                    model != null && string.IsNullOrEmpty(model.PaymentIntegrationProperties) ? value != default : true)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
