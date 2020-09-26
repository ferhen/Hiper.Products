using Hiper.Application.Core.Models;
using Hiper.Application.Data.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hiper.Application.Util.Extensions;

namespace Hiper.Application.Data.SqlServer
{
    public static class IoC
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddScoped(x => new ApplicationUser());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScopedFactory<IUnitOfWork, UnitOfWork>();
        }
    }
}
