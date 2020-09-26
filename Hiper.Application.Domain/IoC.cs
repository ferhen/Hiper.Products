using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Hiper.Application.Domain.Mappings;
using Hiper.Application.Domain.Services;

namespace Hiper.Application.Domain
{
    public static class IoC
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ProductService>();
            services.AddScoped<StockService>();
        }
    }
}
