using AutoMapper;
using Hiper.SynchronizationAPI.Domain.Mappings;
using Hiper.SynchronizationAPI.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiper.SynchronizationAPI.Domain
{
    public static class IoC
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddHostedService<ConsumerService>();

            services.AddScoped<ProductSnapshotService>();
        }
    }
}
