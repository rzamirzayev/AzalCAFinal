using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TicketBookings
{
    public interface ITicketBookingService
    {
        Task<IEnumerable<TicketBookingGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AddTicketBookingResponseDto> AddAsync(AddTicketBookingRequestDto model, CancellationToken cancellationToken = default);
    }
}
