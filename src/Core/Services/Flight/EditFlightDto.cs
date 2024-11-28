using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public class EditFlightDto
    {
        public int FlightId { get; set; }
        public int AirplaneId { get; set; }
        public int DepartureAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public DateOnly FlightTime { get; set; }
    }
}
