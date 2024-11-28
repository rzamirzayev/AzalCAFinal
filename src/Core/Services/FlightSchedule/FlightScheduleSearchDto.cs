using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FlightSchedule
{
    public class FlightScheduleSearchDto
    {

            public int Id { get; set; }
            public int FlightId { get; set; }  
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }


    }
}
