using Autofac;
using JetBrains.Annotations;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.SettingsReader;
using MAVN.Service.PaymentManagement.Contract;
using MAVN.Service.PaymentManagement.Settings;

namespace MAVN.Service.PaymentManagement.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string PubExchangeName = "lykke.payment.completed";

        private readonly RabbitMqSettings _settings;

        public RabbitMqModule(IReloadingManager<AppSettings> settingsManager)
        {
            _settings = settingsManager.CurrentValue.PaymentManagementService.Rabbit;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // NOTE: Do not register entire settings in container, pass necessary settings to services which requires them

            RegisterRabbitMqPublishers(builder);
        }

        // registered publishers could be esolved by IRabbitPublisher<TMessage> interface
        private void RegisterRabbitMqPublishers(ContainerBuilder builder)
        {
            builder.RegisterJsonRabbitPublisher<PaymentCompletedEvent>(
                _settings.Publishers.ConnectionString,
                PubExchangeName);
        }
    }
}
