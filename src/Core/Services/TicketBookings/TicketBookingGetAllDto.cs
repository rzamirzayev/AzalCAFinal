using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TicketBookings
{
    public class TicketBookingGetAllDto
    {
        public int BookingId { get; set; }
        public int PassangerId { get; set; }
        public int FlightId { get; set; }
        public required string CabinClass { get; set; }
        public bool IsChild { get; set; }
        public int Price { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
