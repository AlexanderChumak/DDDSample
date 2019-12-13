using System;
using System.Threading.Tasks;
using Uklon.Ordering.Domain.Errors;
using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.Domain.Riders;
using Uklon.Ordering.DTO;
using Uklon.Ordering.Infrastructure;

namespace Uklon.Ordering.Services
{
    public class CancelOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRiderRepository _riderRepository;
        private readonly INotificationService _notificationService;

        public CancelOrderHandler(
            IOrderRepository orderRepository,
            IRiderRepository riderRepository,
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _riderRepository = riderRepository;
            _notificationService = notificationService;
        }
        public async Task<Either<CancelOrderError, OrderAggregate>> CancelByRider(CancelOrderCommand cancelOrderCommand)
        {
            if (!cancelOrderCommand.Validate())
                return CancelOrderError.From("Bad request.");

            var rider = await _riderRepository.Find(RiderId.From(cancelOrderCommand.RiderId));
            var order = await _orderRepository.Find(OrderId.From(cancelOrderCommand.OrderId));

            var cancellationResult = order.CancelBy(rider, DateTimeOffset.UtcNow);

            await _orderRepository.Update(order);

            await cancellationResult.MapReduce(
                error => WhenCancellationDeclined(rider),
                _ => WhenCancellationOk(order, rider));

            return cancellationResult
                .MapRight(_ => order);
        }

        private Task WhenCancellationOk(OrderAggregate order, RiderAggregate rider) =>
            _notificationService.NotifyRiderAboutOrderCancellation(rider.RiderId);

        private Task WhenCancellationDeclined(RiderAggregate rider) =>
            _notificationService.NotifyRiderAboutOrderCancellation(rider.RiderId);
    }
}
