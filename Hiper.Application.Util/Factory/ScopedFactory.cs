using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hiper.Application.Util.Factory
{
    internal class ScopedFactory<TService, TImplementation> : IFactory<TService>
            where TImplementation : IDisposable, TService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public ScopedFactory(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public TService Create()
        {
            IServiceScope scope = scopeFactory.CreateScope();
            TImplementation service = (TImplementation)scope.ServiceProvider.GetRequiredService<TService>();
            return service;
        }
    }
}
