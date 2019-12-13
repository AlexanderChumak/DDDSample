using System.Collections.Generic;
using System.Linq;

namespace Uklon.Ordering.Domain
{
    public abstract class AggregateRoot<TKey> : DomainEntity<TKey>
        where TKey : ValueObject<TKey>
    {
        private readonly List<DomainEvent> _domainEvents;

        public AggregateVersion InitialVersion { get; protected set; }

        public AggregateVersion Version { get; protected set; }

        protected AggregateRoot()
        {
            _domainEvents = new List<DomainEvent>();
        }

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public IReadOnlyCollection<DomainEvent> GetAndRemoveDomainEvents()
        {
            var copy = _domainEvents.ToArray();

            _domainEvents.Clear();

            return copy;
        }

        public IReadOnlyCollection<DomainEvent> GetDomainEvents()
        {
            var copy = _domainEvents.ToArray();

            return copy;
        }

        protected bool HasEvents => _domainEvents.Any();
    }
}