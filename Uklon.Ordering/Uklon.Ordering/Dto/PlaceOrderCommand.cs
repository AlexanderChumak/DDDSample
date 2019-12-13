using System;

namespace Uklon.Ordering.DTO
{
    public class PlaceOrderCommand
    {
        public Guid RiderId { get; set; }

        public string Phone { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}