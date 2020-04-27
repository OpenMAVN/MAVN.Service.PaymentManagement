using System.Collections.Generic;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentProviderRequirements
    {
        public string PaymentProvider { get; set; }

        public List<PaymentProviderRequirement> Requirements { get; set; }
    }
}
