namespace Uklon.Ordering.Domain.Errors
{
    public class PlaceOrderError
    {
        public string Message { get; }

        private PlaceOrderError(string message)
        {
            Message = message;
        }

        public static PlaceOrderError RiderIsSuspected() => new PlaceOrderError("Place order declined. Rider is suspected");

        public static PlaceOrderError From(string reason) => new PlaceOrderError(reason);
    }

    public class CancelOrderError
    {
        public string Message { get; }

        private CancelOrderError(string message)
        {
            Message = message;
        }

        public static CancelOrderError ToLateForCancellation() => new CancelOrderError("Order cancellation declined. Order has already running");

        public static CancelOrderError RiderIsSuspected() => new CancelOrderError("Order cancellation declined. Rider is suspected");

        public static CancelOrderError From(string reason) => new CancelOrderError(reason);
    }

    public class Ok { }
}
