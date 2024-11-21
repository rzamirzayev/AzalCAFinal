
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
        Task<IEnumerable<FlightGetAllDto>> GetById(int id,CancellationToken cancellationToken = default);
        Task<AddFlightResponseDto> AddAsync(AddFlightRequestDto model, CancellationToken cancellationToken = default);


    }
}
