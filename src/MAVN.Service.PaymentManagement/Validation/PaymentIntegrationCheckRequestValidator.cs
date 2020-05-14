using FluentValidation;
using MAVN.Service.PaymentManagement.Client.Models.Requests;

namespace MAVN.Service.PaymentManagement.Validation
{
    public class PaymentIntegrationCheckRequestValidator : AbstractValidator<PaymentIntegrationCheckRequest>
    {
        public PaymentIntegrationCheckRequestValidator()
        {
            RuleFor(x => x.PartnerId)
                .Must((model, value) =>
                    model != null && string.IsNullOrEmpty(model.PaymentIntegrationProvider) ? value != default : true)
                .WithMessage(x => $"{nameof(x.PartnerId)} required");
        }
    }
}
