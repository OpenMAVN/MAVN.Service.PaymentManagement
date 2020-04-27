using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Available payment providers requirements response model.
    /// </summary>
    [PublicAPI]
    public class AvailablePaymentProvidersRequirementsResponse
    {
        /// <summary>Provider requirements list</summary>
        public List<PaymentProviderProperties> ProvidersRequirements { get; set; }
    }
}
