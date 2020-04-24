using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace MAVN.Service.PaymentManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public PaymentManagementSettings PaymentManagementService { get; set; }
    }
}
