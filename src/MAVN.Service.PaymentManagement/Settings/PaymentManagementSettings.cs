using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PaymentManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PaymentManagementSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings Rabbit { get; set; }
    }
}
