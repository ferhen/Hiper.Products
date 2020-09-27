using Hiper.Util.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Hiper.Util.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, IDisposable, TService
        {
            services.TryAddScoped<TService, TImplementation>();
            services.AddSingleton<IFactory<TService>, ScopedFactory<TService, TImplementation>>();
        }
    }
}
