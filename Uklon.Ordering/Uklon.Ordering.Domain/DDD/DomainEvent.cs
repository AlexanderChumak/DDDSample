using System;

namespace Uklon.Ordering.Domain
{
    public abstract class DomainEvent
    {
        protected DomainEvent(DateTimeOffset occurredAt)
        {
            OccurredAt = occurredAt;
        }

        public DateTimeOffset OccurredAt { get; }
    }
}
