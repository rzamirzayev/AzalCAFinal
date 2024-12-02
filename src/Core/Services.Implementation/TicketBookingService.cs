using Domain.Entities;
using Repositories;
using Services.Services;
using Services.TicketBookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class TicketBookingService : ITicketBookingService
    {
        private readonly ITicketBookingsRepository ticketBookingsRepository;
        public TicketBookingService(ITicketBookingsRepository ticketBookingsRepository) {
            this.ticketBookingsRepository = ticketBookingsRepository;
        }
        public async Task<AddTicketBookingResponseDto> AddAsync(AddTicketBookingRequestDto model, CancellationToken cancellationToken = default)
        {
            var entitiy = new TicketBooking
            {
                PassangerId = model.PassangerId,
                CabinClass = model.CabinClass,
                FlightId = model.FlightId,
                IsChild = model.IsChild,
                Price = model.Price,
                BookingDate = model.BookingDate,
            };
            await ticketBookingsRepository.AddAsync(entitiy, cancellationToken);
            await ticketBookingsRepository.SaveAsync();
            return new AddTicketBookingResponseDto
            {
                BookingId = entitiy.BookingId,
                CabinClass=entitiy.CabinClass,
                FlightId=entitiy.FlightId,
                IsChild=entitiy.IsChild,
                Price = entitiy.Price,
                BookingDate=entitiy.BookingDate.ToString(),
            };


    
        }

        public Task<IEnumerable<TicketBookingGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
