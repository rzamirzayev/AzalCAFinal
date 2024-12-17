using Services.Airplane;
using Services.Airport;
using Services.Flight;

namespace WebUI.Models
{
    public class FlightViewModel
    {

        public List<AirplaneGetAllDto>? Airplanes { get; set; }
        public List<AirportGetAllDto>? Airports { get; set; }
        public int? FlightId { get; set; }
        public int SelectedAirplaneId { get; set; }         
        public int SelectedDepartureAirportId { get; set; }   
        public int SelectedDestinationAirportId { get; set; } 
        public int EconomyPrice { get; set; }                
        public int BusinessPrice { get; set; }             
        public DateOnly FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }         
        public TimeSpan ArrivalTime { get; set; }
    }
    public class FlightSearchViewModel
    {
        public List<FlightSearchDto>? OutboundFlights { get; set; }
        public List<FlightSearchDto>? ReturnFlights { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public string TripType { get; set; }
    }
    public class FlightDetailsViewModel
    {
        public AddFlightResponseDto? OutboundFlights { get; set; }
        public AddFlightResponseDto? ReturnFlights { get; set; }
    }
}
