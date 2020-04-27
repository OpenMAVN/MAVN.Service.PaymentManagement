using Lykke.HttpClientGenerator;
using Lykke.HttpClientGenerator.Infrastructure;

namespace MAVN.Service.PaymentIntegrationPlugin.Client
{
    /// <summary>
    /// PaymentManagement API aggregating interface.
    /// </summary>
    public class PaymentIntegrationPluginClient : IPaymentIntegrationPluginClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to PaymentManagement Api.</summary>
        public IPaymentIntegrationPluginApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public PaymentIntegrationPluginClient(string serviceUrl)
        {
            var clientBuilder = HttpClientGenerator.BuildForUrl(serviceUrl)
                .WithAdditionalCallsWrapper(new ExceptionHandlerCallsWrapper())
                .WithoutRetries();
            var httpClientGenerator = clientBuilder.Create();

            Api = httpClientGenerator.Generate<IPaymentIntegrationPluginApi>();
        }
    }
}
