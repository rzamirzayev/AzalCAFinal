using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Airport
{
    public class AirportGetAllDto
    {
        public int AirportId { get; set; }
        public required string AirportName { get; set; }

    }
}
