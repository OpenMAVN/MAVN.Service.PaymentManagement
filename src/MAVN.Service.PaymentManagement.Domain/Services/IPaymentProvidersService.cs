using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PaymentManagement.Domain.Enums;

namespace MAVN.Service.PaymentManagement.Domain.Services
{
    public interface IPaymentProvidersService
    {
        Task<List<PaymentProviderRequirements>> GetPaymentProvidersRequirementsAsync();

        Task<List<PaymentProviderSupportedCurrencies>> GetSupportedCurrenciesAsync(string paymentProvider, Guid? partnerId);

        Task<PaymentIntegrationCheckErrorCodes> CheckPaymentIntegrationAsync(Guid partnerId,
            string paymentIntegrationProperties, string paymentIntegrationProvider);

        Task<PaymentGenerationResult> GeneratePaymentAsync(GeneratePaymentData data);

        Task<string> ValidatePaymentAsync(PaymentValidationData data);
    }
}
