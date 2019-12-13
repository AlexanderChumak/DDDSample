using System.Threading.Tasks;
using Uklon.Ordering.Domain.Orders;

namespace Uklon.Ordering.Domain.Riders
{
    public interface IRiderRepository
    {
        Task<RiderAggregate> Find(RiderId id);

        Task Insert(RiderAggregate order);

        Task Update(RiderAggregate order);
    }
}
