using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public required string CityName { get; set; }

        public Country? Country { get; set; }  
        public ICollection<Airport> Airports { get; set; } = new List<Airport>(); 
    }

}
