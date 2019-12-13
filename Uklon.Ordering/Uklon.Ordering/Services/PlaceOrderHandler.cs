using System.Threading.Tasks;
using Uklon.Ordering.Domain.Errors;
using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.Domain.Riders;
using Uklon.Ordering.DTO;
using Uklon.Ordering.Factories;
using Uklon.Ordering.Infrastructure;

namespace Uklon.Ordering.Services
{
    public class PlaceOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRiderRepository _riderRepository;
        private readonly INotificationService _notificationService;

        public PlaceOrderHandler(
            IOrderRepository orderRepository,
            IRiderRepository riderRepository,
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _riderRepository = riderRepository;
            _notificationService = notificationService;
        }
        public async Task<Either<PlaceOrderError, OrderAggregate>> Place(PlaceOrderCommand placeOrderCommand)
        {
            if (!placeOrderCommand.Validate())
                return PlaceOrderError.From("Bad request.");

            var rider = await _riderRepository.Find(RiderId.From(placeOrderCommand.RiderId));

            if (rider.IsSuspected)
            {
                return PlaceOrderError.RiderIsSuspected();
            }

            var order = OrderFactory.Create(placeOrderCommand);

            await _orderRepository.Insert(order);

            await _notificationService.NotifyRiderAboutOrder(rider.RiderId);

            return order;
        }
    }
}
