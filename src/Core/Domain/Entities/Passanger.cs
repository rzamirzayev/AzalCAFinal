using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Passenger
    {
        public int PassangerId { get; set; }
        public required string FullName { get; set; }
        public required string FinCode { get; set; }
        public required DateTime DateOfBirth { get; set; }

        public ICollection<TicketBooking> TicketBookings { get; set; } = new List<TicketBooking>();
    }

}
