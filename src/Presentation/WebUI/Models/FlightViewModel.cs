using Services.Airplane;
using Services.Airport;
using Services.Flight;

namespace WebUI.Models
{
    public class FlightViewModel
    {

        public List<AirplaneGetAllDto> Airplanes { get; set; }
        public List<AirportGetAllDto> Airports { get; set; }
        public int SelectedAirplaneId { get; set; }         
        public int SelectedDepartureAirportId { get; set; }   
        public int SelectedDestinationAirportId { get; set; } 
        public int EconomyPrice { get; set; }                
        public int BusinessPrice { get; set; }             
        public DateOnly FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }         
        public TimeSpan ArrivalTime { get; set; }
    }
}
