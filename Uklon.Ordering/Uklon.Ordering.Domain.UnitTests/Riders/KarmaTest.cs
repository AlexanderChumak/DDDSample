using NUnit.Framework;
using Uklon.Ordering.Domain.Riders;

namespace Uklon.Ordering.Domain.UnitTests.Riders
{
    [TestFixture]
    public class KarmaTest
    {
        private const int MaxKarma = 10;
        [Test]
        public void When_CreateNew_Then_HasMaxValue()
        {
            var karma = Karma.New();

            Assert.That(karma.Value, Is.EqualTo(10));
        }

        [Test]
        public void Given_MaxKarma_When_Up_Then_HasMaxValue()
        {
            var karma = Karma.From(10);
            karma = karma.Up();

            Assert.That(karma.Value, Is.EqualTo(MaxKarma));
        }

        [Test]
        public void Given_Karma_When_Down_Then_Decrement()
        {
            var karma = Karma.From(5);
            karma = karma.Down();

            Assert.That(karma.Value, Is.EqualTo(4));
        }
    }
}
