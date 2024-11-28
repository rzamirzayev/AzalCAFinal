using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public class FlightSearchDto
    {
            public int Id { get; set; }
            public string DepartureAirport { get; set; }  
            public string DestinationAirport { get; set; }
            public string FlightDate { get; set; }  
            public List<FlightScheduleDto> FlightSchedules { get; set; }  
        public int EconomyPrice { get; set; }
        public int BusinessPrice { get; set; }
        public string Airplane { get; set; }
        

    }
}
