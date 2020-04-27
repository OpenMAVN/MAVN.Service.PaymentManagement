using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Payment integration supported currencies model
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationSupportedCurrencies
    {
        /// <summary>
        /// Payment provider
        /// </summary>
        public string PaymentProvider { get; set; }

        /// <summary>
        /// Supported currencies
        /// </summary>
        public List<string> SupportedCurrencies { get; set; }
    }
}
