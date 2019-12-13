using System;

namespace Uklon.Ordering.DTO
{
    public class CancelOrderCommand
    {
        public Guid RiderId { get; set; }

        public Guid OrderId { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
