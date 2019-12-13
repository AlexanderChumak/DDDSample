using System;
using Uklon.Ordering.Domain.Orders;

namespace Uklon.Ordering.Domain.Riders
{
    public class RiderKarmaChanged : DomainEvent
    {
        public RiderKarmaChanged(RiderId riderId, Karma karma, DateTimeOffset occurredAt) : base(occurredAt)
        {
            RiderId = riderId;
            Karma = karma;
        }

        public RiderId RiderId { get; }
        public Karma Karma { get; }
    }
}
