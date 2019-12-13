using System.Threading.Tasks;
using Uklon.Ordering.Domain;

namespace Uklon.Ordering.Infrastructure
{
    public interface IConsumer<T> where T: DomainEvent
    {
        Task Handle(T domainEvent);
    }
}
