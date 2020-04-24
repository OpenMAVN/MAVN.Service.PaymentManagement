using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PaymentManagement.Client 
{
    /// <summary>
    /// PaymentManagement client settings.
    /// </summary>
    public class PaymentManagementServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
