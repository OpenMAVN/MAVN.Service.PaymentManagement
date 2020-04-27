using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAVN.Service.PaymentManagement.Domain.Services
{
    public interface IPaymentProvidersService
    {
        Task<List<PaymentProviderRequirements>> GetPaymentProvidersRequirementsAsync();
    }
}
