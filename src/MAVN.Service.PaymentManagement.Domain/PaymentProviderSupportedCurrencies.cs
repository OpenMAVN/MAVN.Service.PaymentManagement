using System.Collections.Generic;

namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentProviderSupportedCurrencies
    {
        public string PaymentProvider { get; set; }

        public List<string> SupportedCurrencies { get; set; }
    }
}
