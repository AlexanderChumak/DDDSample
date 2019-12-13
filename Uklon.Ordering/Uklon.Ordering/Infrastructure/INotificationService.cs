using System.Threading.Tasks;
using Uklon.Ordering.Domain.Orders;

namespace Uklon.Ordering.Infrastructure
{
    public interface INotificationService
    {
        Task NotifyRiderAboutOrder(RiderId riderId);

        Task NotifyRiderAboutOrderCancellation(RiderId riderId);
        
        Task NotifyRiderAboutOrderCancellationDeclined(RiderId riderId);
    }
}
