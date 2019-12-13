using System;
using System.Collections.Generic;
using System.Text;

namespace Uklon.Ordering.Domain.Orders
{
    public sealed class RiderId : ValueObject<RiderId>
    {
        private const int UID_DB_LENGTH = 33;

        private RiderId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Can't be empty.", nameof(id));
            }

            Id = id;
        }

        public Guid Id { get; }

        public static RiderId New()
        {
            return new RiderId(Guid.NewGuid());
        }

        public static RiderId From(Guid uid)
        {
            return new RiderId(uid);
        }

        public override string ToString()
        {
            return Id.ToString("N");
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Id;
        }
    }
}
