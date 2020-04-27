using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment integration properties response model
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationPropertiesResponse
    {
        /// <summary>Properties list</summary>
        public List<PaymentIntegrationProperty> Properties { get; set; }
    }
}
