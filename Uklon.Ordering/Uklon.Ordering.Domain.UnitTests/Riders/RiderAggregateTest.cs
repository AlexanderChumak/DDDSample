using System;
using System.Linq;
using NUnit.Framework;
using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.Domain.Riders;

namespace Uklon.Ordering.Domain.UnitTests.Riders
{
    [TestFixture()]
    public sealed class RiderAggregateTest
    {
        [Test]
        public void GivenRider_WhenKarmaChanged_ThenRaiseEvent()
        {
            var rider = new RiderAggregate(RiderId.New(), Karma.From(4));

            rider.ImproveKarmaAfterSuccessfulOrder(Now);

            Assert.True(rider.GetDomainEvents().Any(o => o is RiderKarmaChanged));
        }

        [Test]
        public void GivenRiderWithMaxKarma_WhenTryImproveKarma_ThenKarmaDoesntChange()
        {
            var rider = new RiderAggregate(RiderId.New(), Karma.From(99999));

            rider.ImproveKarmaAfterSuccessfulOrder(Now);

            Assert.True(rider.GetDomainEvents().All(o => !(o is RiderKarmaChanged)));
        }

        [Test]
        public void GivenRiderWithZeroKarma_WhenReduceKarma_ThenSuspectRider()
        {
            var rider = new RiderAggregate(RiderId.New(), Karma.Zero);

            rider.ReduceKarmaAfterCanceledOrder(Now);

            Assert.That(rider.IsSuspected);
        }

        private DateTimeOffset Now => DateTimeOffset.Now;
    }
}
