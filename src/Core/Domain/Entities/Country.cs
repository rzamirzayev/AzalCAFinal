using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public required string CountryName { get; set; }
        public required string CountryCode { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();  // Bir ülkenin birden fazla şehri olabilir
    }
}
