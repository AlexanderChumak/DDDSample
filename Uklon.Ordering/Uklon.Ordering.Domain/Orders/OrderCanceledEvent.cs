using System;

namespace Uklon.Ordering.Domain.Orders
{
    public sealed class OrderCanceledEvent : DomainEvent
    {
        public OrderCanceledEvent(OrderId orderId, RiderId riderId) : base(DateTimeOffset.UtcNow)
        {
            OrderId = orderId;
            RiderId = riderId;
        }
        public OrderId OrderId{ get; }

        public RiderId RiderId { get; }
    }
}
