using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public class AddFlightRequestDto
    {
        public int AirplaneId { get; set; }
        public int DepartureAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public DateTime FlightDate { get; set; }
    }
    public class AddFlightResponseDto
    {
        public int FlightId { get; set; }
        public string? AirplaneName { get; set; }
        public string? DepartureAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public DateTime FlightDate { get; set; }

    }


}
