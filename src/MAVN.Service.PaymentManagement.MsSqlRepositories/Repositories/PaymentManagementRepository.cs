using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using MAVN.Service.PaymentManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Repositories
{
    public class PaymentManagementRepository : IPaymentManagementRepository
    {
        private readonly IDbContextFactory<PaymentManagementContext> _contextFactory;

        public PaymentManagementRepository(IDbContextFactory<PaymentManagementContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
