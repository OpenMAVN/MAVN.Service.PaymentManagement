using Lykke.HttpClientGenerator;

namespace MAVN.Service.PaymentManagement.Client
{
    /// <summary>
    /// PaymentManagement API aggregating interface.
    /// </summary>
    public class PaymentManagementClient : IPaymentManagementClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to PaymentManagement Api.</summary>
        public IPaymentManagementApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public PaymentManagementClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IPaymentManagementApi>();
        }
    }
}
