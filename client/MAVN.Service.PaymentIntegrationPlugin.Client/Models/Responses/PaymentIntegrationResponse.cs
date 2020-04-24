using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment integration response model
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationResponse
    {
        /// <summary>Properties list</summary>
        public List<PaymentIntegrationProperty> Properties { get; set; }
    }
}
