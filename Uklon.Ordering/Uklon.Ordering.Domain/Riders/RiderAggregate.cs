using System;
using Uklon.Ordering.Domain.Orders;

namespace Uklon.Ordering.Domain.Riders
{
    public class RiderAggregate : AggregateRoot<RiderId>
    {
        public RiderAggregate(RiderId riderId, Karma karma)
        {
            RiderId = riderId;
            Karma = karma;
        }

        public RiderId RiderId { get; }

        public Karma Karma { get; private set; }

        public bool IsSuspected => Karma.IsLessZero;

        public void ImproveKarmaAfterSuccessfulOrder(DateTimeOffset atTime)
        {
            var changedKarma = Karma.Up();

            TryChangeKarma(changedKarma, atTime);
        }

        public void ReduceKarmaAfterCanceledOrder(DateTimeOffset atTime)
        {
            var changedKarma = Karma.Down();

            TryChangeKarma(changedKarma, atTime);
        }

        private void TryChangeKarma(Karma changedKarma, DateTimeOffset atTime)
        {
            if (changedKarma != Karma)
            {
                var changedKarmaEvent = new RiderKarmaChanged(Id, changedKarma, atTime);
                Apply(changedKarmaEvent);
                AddDomainEvent(changedKarmaEvent);
            }
        }

        private void Apply(RiderKarmaChanged riderKarmaChanged)
        {
            Karma = riderKarmaChanged.Karma;
        }
    }
}
