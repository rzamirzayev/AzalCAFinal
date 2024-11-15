using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FlightSchedule
    {
        public int ScheduleId { get; set; }
        public int FlightId { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public Flight? Flight { get; set; }  
    }

}
