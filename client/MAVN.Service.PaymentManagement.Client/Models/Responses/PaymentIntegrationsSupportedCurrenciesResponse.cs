using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Payment integrations supported currencies response model
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationsSupportedCurrenciesResponse
    {
        /// <summary>
        /// Supported currencies for payment providers
        /// </summary>
        public List<PaymentIntegrationSupportedCurrencies> ProvidersSupportedCurrencies { get; set; }
    }
}
