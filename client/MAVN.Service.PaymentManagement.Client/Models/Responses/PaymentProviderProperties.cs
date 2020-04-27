using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Payment provider properties response model
    /// </summary>
    [PublicAPI]
    public class PaymentProviderProperties
    {
        /// <summary>Properties list</summary>
        public List<PaymentProviderProperty> Properties { get; set; }

        /// <summary>Payment provider</summary>
        public string PaymentProvider { get; set; }
    }
}
