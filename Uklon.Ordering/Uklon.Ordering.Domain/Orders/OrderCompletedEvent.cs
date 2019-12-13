using System;

namespace Uklon.Ordering.Domain.Orders
{
    public sealed class OrderCompletedEvent : DomainEvent
    {
        public OrderCompletedEvent(OrderId orderId, RiderId riderId) : base(DateTimeOffset.UtcNow)
        {
            OrderId = orderId;
            RiderId = riderId;
        }
        public OrderId OrderId{ get; }

        public RiderId RiderId { get; }
    }
}
