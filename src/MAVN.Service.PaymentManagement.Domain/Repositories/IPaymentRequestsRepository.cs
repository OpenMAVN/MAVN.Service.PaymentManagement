using System;
using System.Threading.Tasks;

namespace MAVN.Service.PaymentManagement.Domain.Repositories
{
    public interface IPaymentRequestsRepository
    {
        Task<IPaymentRequest> GetById(Guid paymentRequestId);

        Task CreateAsync(IPaymentRequest paymentRequest);

        Task UpdateAsync(IPaymentRequest paymentRequest);

        Task<IPaymentRequest> GetByExternalId(string externalId);
    }
}
