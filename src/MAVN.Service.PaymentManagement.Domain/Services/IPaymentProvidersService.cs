using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAVN.Service.PaymentManagement.Domain.Services
{
    public interface IPaymentProvidersService
    {
        Task<List<PaymentProviderRequirements>> GetPaymentProvidersRequirementsAsync();

        Task<List<PaymentProviderSupportedCurrencies>> GetSupportedCurrenciesAsync(string paymentProvider, Guid? partnerId);

        Task<bool> CheckPaymentIntegrationAsync(Guid partnerId);

        Task<PaymentGenerationResult> GeneratePaymentAsync(GeneratePaymentData data);

        Task ValidatePaymentAsync(PaymentValidationData data);
    }
}
