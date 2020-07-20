﻿using System.Data.Common;
using JetBrains.Annotations;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.PaymentManagement.Domain.Enums;
using MAVN.Service.PaymentManagement.MsSqlRepositories.Entities;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories
{
    public class PaymentManagementContext : PostgreSQLContext
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

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.CustomerId);

            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.PartnerId);

            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.Currency);

            modelBuilder.Entity<PaymentRequestEntity>()
                .HasIndex(p => p.ExternalPaymentEntityId)
                .IsUnique(false);

            modelBuilder.Entity<PaymentRequestEntity>().Property(c => c.PaymentStatus)
                .HasConversion(new EnumToStringConverter<PaymentStatus>());
        }
    }
}
