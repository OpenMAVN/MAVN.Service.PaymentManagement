using System.Data.Common;
using JetBrains.Annotations;
using MAVN.Common.MsSql;
using MAVN.Service.PaymentManagement.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories
{
    public class PaymentManagementContext : MsSqlContext
    {
        private const string Schema = "payment";

        internal DbSet<PaymentRequestEntity> PaymentRequests { get; set; }

        // empty constructor needed for EF migrations
        [UsedImplicitly]
        public PaymentManagementContext()
            : base(Schema)
        {
        }

        public PaymentManagementContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        //Needed constructor for using InMemoryDatabase for tests
        public PaymentManagementContext(DbContextOptions options)
            : base(Schema, options)
        {
        }

        public PaymentManagementContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.CustomerId);

            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.PartnerId);

            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.Currency);
        }
    }
}
