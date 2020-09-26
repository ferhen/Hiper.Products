using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Hiper.Application.Core.Models
{
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset LastUpdateOn { get; set; } = DateTimeOffset.Now;

        [MaxLength(256)]
        public string LastUpdatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [IgnoreDataMember]
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
