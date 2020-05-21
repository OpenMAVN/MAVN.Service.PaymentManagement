using System;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Common.MsSql;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.Domain.Repositories;
using MAVN.Service.PaymentManagement.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Repositories
{
    public class PaymentRequestsRepository : IPaymentRequestsRepository
    {
        private readonly IDbContextFactory<PaymentManagementContext> _contextFactory;
        private readonly IMapper _mapper;

        public PaymentRequestsRepository(
            IDbContextFactory<PaymentManagementContext> contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task CreateAsync(IPaymentRequest paymentRequest)
        {
            var entity = _mapper.Map<PaymentRequestEntity>(paymentRequest);

            using (var context = _contextFactory.CreateDataContext())
            {
                context.PaymentRequests.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<IPaymentRequest> GetById(Guid paymentRequestId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PaymentRequests.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == paymentRequestId);
                return result;
            }
        }

        public async Task<IPaymentRequest> GetByExternalId(string externalId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PaymentRequests.AsNoTracking().FirstOrDefaultAsync(_ => _.ExternalPaymentEntityId == externalId);
                return result;
            }
        }

        public async Task UpdateAsync(IPaymentRequest paymentRequest)
        {
            var entity = _mapper.Map<PaymentRequestEntity>(paymentRequest);

            using (var context = _contextFactory.CreateDataContext())
            {
                context.PaymentRequests.Update(entity);

                await context.SaveChangesAsync();
            }
        }
    }
}
