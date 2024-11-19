
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
