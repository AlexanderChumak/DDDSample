using System;
using System.Collections.Generic;

namespace Uklon.Ordering.Domain
{
    public sealed class AggregateVersion : ValueObject<AggregateVersion>, IComparable<AggregateVersion>
    {
        public static readonly AggregateVersion Latest = new AggregateVersion(LATEST_VERSION_NUMBER);

        private const int START_VERSION_NUMBER = 0;
        private const int LATEST_VERSION_NUMBER = int.MaxValue;

        private AggregateVersion(int version)
        {
            if (version < START_VERSION_NUMBER)
            {
                throw new ArgumentOutOfRangeException(nameof(version));
            }

            Value = version;
        }

        public int Value { get; }

        public static AggregateVersion From(int version)
        {
            return new AggregateVersion(version);
        }

        public static AggregateVersion New()
        {
            return new AggregateVersion(START_VERSION_NUMBER);
        }

        public AggregateVersion Increment()
        {
            return new AggregateVersion(Value + 1);
        }

        public int CompareTo(AggregateVersion other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString("D");
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}