using System.Linq;
using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.SettingsReader;
using MAVN.Service.PaymentManagement.Domain.Services;
using MAVN.Service.PaymentManagement.DomainServices;
using MAVN.Service.PaymentManagement.Services;
using MAVN.Service.PaymentManagement.Settings;
using StackExchange.Redis;

namespace MAVN.Service.PaymentManagement.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly PaymentManagementSettings _settings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _settings = appSettings.CurrentValue.PaymentManagementService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<PaymentProvidersService>()
                .As<IPaymentProvidersService>()
                .SingleInstance()
                .WithParameter(
                    TypedParameter.From(
                        _settings.PaymentProviderIntegrationPlugins.Select(i => (i.Name, i.IntegrationPluginUrl))))
                .WithParameter(TypedParameter.From(_settings.DefaultPaymentProvider));

            builder.Register(context =>
                {
                    var connectionMultiplexer = ConnectionMultiplexer.Connect(_settings.RedisConnectionString);
                    connectionMultiplexer.IncludeDetailInExceptions = false;
                    return connectionMultiplexer;
                })
                .As<IConnectionMultiplexer>()
                .SingleInstance();
        }
    }
}
