using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PaymentIntegrationPlugin.Client;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.Domain.Services;

namespace MAVN.Service.PaymentManagement.DomainServices
{
    public class PaymentProvidersService : IPaymentProvidersService
    {
        private readonly string _defaultProvider;
        private readonly Dictionary<string, string> _pluginsDict;
        private readonly IMapper _mapper;

        public PaymentProvidersService(
            IEnumerable<(string name, string url)> integrationPlugins,
            string defaultProvider,
            IMapper mapper)
        {
            _pluginsDict = integrationPlugins.ToDictionary(i => i.name, i => i.url);
            _defaultProvider = defaultProvider;
            _mapper = mapper;
        }

        public async Task<List<PaymentProviderRequirements>> GetPaymentProvidersRequirementsAsync()
        {
            var result = new List<PaymentProviderRequirements>();

            foreach (var pair in _pluginsDict)
            {
                var paymentProviderIntegrationPluginClient = new PaymentIntegrationPluginClient(pair.Value);
                var pluginRequirements = await paymentProviderIntegrationPluginClient.Api.GetPaymentIntegrationPropertiesAsync();
                result.Add(
                    new PaymentProviderRequirements
                    {
                        PaymentProvider = pair.Key,
                        Requirements = _mapper.Map<List<PaymentProviderRequirement>>(pluginRequirements.Properties),
                    });
            }

            return result;
        }
    }
}
