using System;
using Uklon.Ordering.Domain.Errors;
using Uklon.Ordering.Domain.Riders;

namespace Uklon.Ordering.Domain.Orders
{
    public class OrderAggregate : AggregateRoot<OrderId>
    {
        public OrderAggregate(RiderId riderId)
        {
            RiderId = riderId;
        }

        public RiderId RiderId { get; }

        public DateTimeOffset PickupTime { get; set; }

        public Either<CancelOrderError, Ok> CancelBy(RiderAggregate rider, DateTimeOffset atTime)
        {
            if(PickupTime < atTime)
                return CancelOrderError.ToLateForCancellation();

            if(rider.IsSuspected)
                return CancelOrderError.RiderIsSuspected();

            var canceledEvent = new OrderCanceledEvent(Id, RiderId);
            Apply(canceledEvent);
            AddDomainEvent(canceledEvent);

            return new Ok();
        }

        public void Complete()
        {
            var completedEvent = new OrderCompletedEvent(Id, RiderId);
            Apply(completedEvent);
            AddDomainEvent(completedEvent);
        }

        private void Apply(OrderCompletedEvent completedEvent)
        {
            Status = OrderStatus.Completed;
        }

        private void Apply(OrderCanceledEvent canceledEvent)
        {
            Status = OrderStatus.Canceled;
        }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Processing,
        Canceled,
        Completed,
    }
}
