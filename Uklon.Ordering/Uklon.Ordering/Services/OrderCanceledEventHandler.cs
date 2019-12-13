using System;
using System.Threading.Tasks;
using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.Domain.Riders;
using Uklon.Ordering.Infrastructure;

namespace Uklon.Ordering.Services
{
    public class OrderCanceledEventHandler : IConsumer<OrderCanceledEvent>
    {
        private readonly IRiderRepository _riderRepository;

        public OrderCanceledEventHandler(IRiderRepository riderRepository)
        {
            _riderRepository = riderRepository;
        }

        public async Task Handle(OrderCanceledEvent domainEvent)
        {
            var rider = await _riderRepository.Find(domainEvent.RiderId);
            rider.ReduceKarmaAfterCanceledOrder(DateTimeOffset.UtcNow);
            await _riderRepository.Update(rider);
        }
    }

    public class OrderCompletedEventHandler : IConsumer<OrderCompletedEvent>
    {
        private readonly IRiderRepository _riderRepository;

        public OrderCompletedEventHandler(IRiderRepository riderRepository)
        {
            _riderRepository = riderRepository;
        }

        public async Task Handle(OrderCompletedEvent domainEvent)
        {
            var rider = await _riderRepository.Find(domainEvent.RiderId);
            rider.ImproveKarmaAfterSuccessfulOrder(DateTimeOffset.UtcNow);
            await _riderRepository.Update(rider);
        }
    }
}
