using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.DTO;

namespace Uklon.Ordering.Factories
{
    public static class OrderFactory
    {
        public static OrderAggregate Create(this PlaceOrderCommand placeOrderCommand)
        {
            return new OrderAggregate(
                RiderId.From(placeOrderCommand.RiderId));
        }
    }
}
