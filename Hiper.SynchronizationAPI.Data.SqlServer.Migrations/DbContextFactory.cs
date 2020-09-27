using Hiper.SynchronizationAPI.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Hiper.SynchronizationAPI.Data.SqlServer.Migrations
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=Hiper.SynchronizationAPI;Integrated Security=True; MultipleActiveResultSets = True;";

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
