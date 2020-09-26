using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Hiper.Application.Data.SqlServer.Migrations
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=Hiper.Application;Integrated Security=True; MultipleActiveResultSets = True; App=HarborShippingIntegration;";

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));

            return new ApplicationDbContext(
                builder.Options,
                new OperationalStoreOptionsMigrations());
        }
    }

    public class OperationalStoreOptionsMigrations : IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions()
        {
            DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
            EnableTokenCleanup = false,
            PersistedGrants = new TableConfiguration("PersistedGrants"),
            TokenCleanupBatchSize = 100,
            TokenCleanupInterval = 3600,
        };
    }
}
