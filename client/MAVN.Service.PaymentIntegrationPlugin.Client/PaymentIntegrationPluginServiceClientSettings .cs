using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PaymentIntegrationPlugin.Client
{
    /// <summary>
    /// PaymentManagement client settings.
    /// </summary>
    public class PaymentIntegrationPluginServiceClientSettings
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl { get; set; }
    }
}
