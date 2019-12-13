using System.Threading.Tasks;

namespace Uklon.Ordering.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<OrderAggregate> Find(OrderId id);

        Task Insert(OrderAggregate order);

        Task Update(OrderAggregate order);
    }

}
