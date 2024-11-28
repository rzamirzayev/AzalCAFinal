using Services.Passanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public class FlightGetAllDto
    {
        public int FlightId { get; set; }
        public string? AirplaneName { get; set; }
        public int? AirplaneId { get; set; }
        public string? DepartureAirportName { get; set; }
        public int? DepartureAirportId { get; set; }
        public string? DestinationAirportName { get; set; }
        public int? DestinationAirportId { get; set; }
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }

        public DateOnly FlightDate { get; set; }
        public List<FlightScheduleDto> FlightSchedules { get; set; } = new List<FlightScheduleDto>(); 
        public List<PassangerGetAllDto> Passangers { get; set; }=new List<PassangerGetAllDto>();
    }

    public class FlightScheduleDto
    {
#warning silinecek iki property
        public int Id { get; set; }
        public int FlightId { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }

}
