using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Airplane
{
    public class AirplaneGetAllDto
    {
        public int AirplaneId { get; set; }
        public required string AirplaneName { get; set; }

    }
}
