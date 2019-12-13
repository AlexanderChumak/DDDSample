using Uklon.Ordering.Domain.Errors;
using Uklon.Ordering.Domain.Orders;
using Uklon.Ordering.Domain.Riders;

namespace Uklon.Ordering.Domain.Services
{
    public sealed class CancelOrderConsent
    {
        public Either<CancelOrderError, Ok> CancelByRider(OrderAggregate order, RiderAggregate rider)
        {
            if (rider.IsSuspected)
                return CancelOrderError.RiderIsSuspected();

            return new Ok();
        }
    }
}
