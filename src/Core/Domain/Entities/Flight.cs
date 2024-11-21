using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int AirplaneId { get; set; }
        public int DepartureAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }
        public required DateTime FlightDate { get; set; }


        public Airplane? Airplane { get; set; }
        public Airport? DepartureAirport { get; set; }
        public Airport? DestinationAirport { get; set; }
        public ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
        public ICollection<TicketBooking> TicketBookings { get; set; } = new List<TicketBooking>();
    }

}
