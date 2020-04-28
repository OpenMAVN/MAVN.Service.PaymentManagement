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
        /// <summary>Payment provider</summary>
        public string PaymentProvider { get; set; }

        /// <summary>Properties list</summary>
        public List<PaymentIntegrationProperty> Properties { get; set; }
    }
}
