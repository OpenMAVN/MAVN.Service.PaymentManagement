using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAVN.Service.PaymentManagement.Domain.Repositories
{
    public interface IPaymentRequestsRepository
    {
        Task<IPaymentRequest> GetById(string paymentRequestId);

        Task CreateAsync(IPaymentRequest paymentRequest);

        Task UpdateAsync(IPaymentRequest paymentRequest);
    }
}
