using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.SettingsReader;
using MAVN.Service.PaymentManagement.Domain.Repositories;
using MAVN.Service.PaymentManagement.MsSqlRepositories;
using MAVN.Service.PaymentManagement.MsSqlRepositories.Repositories;
using MAVN.Service.PaymentManagement.Settings;

namespace MAVN.Service.PaymentManagement.Modules
{
    [UsedImplicitly]
    public class DbModule : Module
    {
        private readonly string _connectionString;

        public DbModule(IReloadingManager<AppSettings> appSettings)
        {
            _connectionString = appSettings.CurrentValue.PaymentManagementService.Db.SqlDbConnString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentRequestsRepository>()
                .As<IPaymentRequestsRepository>()
                .SingleInstance();

            builder.RegisterMsSql(
                _connectionString,
                connString => new PaymentManagementContext(connString, false),
                dbConn => new PaymentManagementContext(dbConn));
        }
    }
}
