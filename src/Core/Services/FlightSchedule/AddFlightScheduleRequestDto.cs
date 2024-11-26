using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FlightSchedule
{
    public class AddFlightScheduleRequestDto
    {
        public TimeSpan DepertureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
    public class AddFlightScheduleResponseDto
    {
        public string FlightScheduleId { get; set; }
        public TimeSpan DepertureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
}
