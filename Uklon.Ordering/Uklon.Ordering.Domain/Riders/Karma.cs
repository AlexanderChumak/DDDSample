using System;
using System.Collections.Generic;

namespace Uklon.Ordering.Domain.Riders
{
    public sealed class Karma : ValueObject<Karma>
    {
        private const int MaxKarma = 10;
        private const int MinKarma = -10;
        private Karma(int value)
        {
            if (value > 10)
                throw new ArgumentOutOfRangeException();

            Value = value;
        }

        public int Value { get; }
        public bool IsLessZero => Value < 0;

        public static Karma Zero = Karma.From(0);

        public Karma Up() => From(Value + 1);

        public Karma Down() => From(Value - 1);

        public static Karma New() => From(MaxKarma);

        public static Karma From(int value)
        {
            if (value < MinKarma)
                value = MinKarma;
            else if (value > MaxKarma)
                value = MaxKarma;

            return new Karma(value);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}
