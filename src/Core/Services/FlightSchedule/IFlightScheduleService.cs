
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FlightSchedule
{
    public interface IFlightScheduleService
    {
        Task<IEnumerable<FlightScheduleGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<FlightScheduleGetAllDto> GetById(int id, CancellationToken cancellationToken = default);

        //Task<AddFlightScheduleResponseDto> AddAsync(AddFlightScheduleRequestDto model, CancellationToken cancellationToken = default);

    }
}
