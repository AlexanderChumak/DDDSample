using System;
using System.Collections.Generic;
using System.Text;

namespace Uklon.Ordering.Domain.Orders
{
    public sealed class OrderId : ValueObject<OrderId>
    {
        private const int UID_DB_LENGTH = 33;

        private OrderId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Can't be empty.", nameof(id));
            }

            Id = id;
        }

        public Guid Id { get; }

        public static OrderId New()
        {
            return new OrderId(Guid.NewGuid());
        }

        public static OrderId From(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(orderId));
            }

            return FromDbId(orderId);
        }

        private static OrderId FromDbId(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(orderId));
            }

            return orderId.Length == UID_DB_LENGTH
                ? new OrderId(Guid.Parse(orderId.Substring(0, UID_DB_LENGTH - 1)))
                : new OrderId(Guid.Parse(orderId));
        }

        public static OrderId From(Guid uid)
        {
            return new OrderId(uid);
        }

        public string ToDbUid()
        {
            return Id.ToString("N") + "n";
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
