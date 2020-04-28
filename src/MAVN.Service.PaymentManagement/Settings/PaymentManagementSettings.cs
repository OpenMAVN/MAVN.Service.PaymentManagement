using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PaymentManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PaymentManagementSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings Rabbit { get; set; }

        public List<PaymentProviderSettings> PaymentProviderIntegrationPlugins { get; set; }

        public string DefaultPaymentProvider { get; set; }

        public string PaymentSuccessUrlTemplate { get; set; }

        public string PaymentFailUrlTemplate { get; set; }

        public string RedisConnectionString { get; set; }
    }
}
