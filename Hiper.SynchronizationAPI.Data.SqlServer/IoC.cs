using Hiper.SynchronizationAPI.Data.SqlServer.Context;
using Hiper.SynchronizationAPI.Data.SqlServer.Repositories;
using Hiper.Util.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hiper.SynchronizationAPI.Data.SqlServer
{
    public static class IoC
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScopedFactory<IUnitOfWork, UnitOfWork>();
        }
    }
}
