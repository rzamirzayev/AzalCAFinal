using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Airplane 
    {
        public int AirplaneId { get; set; }
        public required string AirplaneName { get; set; }
        public int TotalCapacity { get; set; }
        public int EconomyCapacity { get; set; }
        public int BusinessCapacity { get; set; }

        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }

}
