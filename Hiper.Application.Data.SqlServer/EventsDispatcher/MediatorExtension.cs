using Hiper.Application.Core.Models;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Hiper.Application.Data.SqlServer.EventsDispatcher
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<ModelBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            if (!(domainEntities.Any()))
                return;

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
