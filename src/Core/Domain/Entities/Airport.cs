using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Airport 
    {
        public int AirportId { get; set; }
        public required string AirportName { get; set; }
        public int CityId { get; set; }

        public City? City { get; set; }
        public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> DestinationFlights { get; set; } = new List<Flight>();
    }

}
