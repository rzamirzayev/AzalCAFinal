using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
                TicketNumber=model.TicketNumber,
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
                TicketNumber=entitiy.TicketNumber,
                BookingDate=entitiy.BookingDate.ToString(),
            };


    
        }

        public async Task<IEnumerable<TicketBookingGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await ticketBookingsRepository.GetAll().Select(m => new TicketBookingGetAllDto
            {
                BookingDate = m.BookingDate,
                FlightId = m.FlightId,
                CabinClass = m.CabinClass,
                IsChild = m.IsChild,
                Price = m.Price,
                TicketNumber=m.TicketNumber,
                PassangerId=m.PassangerId,
                BookingId=m.BookingId,
                
    }).ToListAsync(cancellationToken);
            return data;
        }

        public async Task<AddTicketBookingResponseDto> GetTicketNumber(string surname, string number)
        {
            var ticketBookings = await ticketBookingsRepository
                .GetAll()
                .Include(m => m.Passenger) 
                .ToListAsync();

    
            var matchingTicket = ticketBookings
                .FirstOrDefault(f => f.Passenger.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase) && f.TicketNumber == number);

            
            if (matchingTicket == null)
            {
              
                throw new Exception("Ticket not found.");
            }

            
            var responseDto = new AddTicketBookingResponseDto
            {
                TicketNumber = matchingTicket.TicketNumber,
                PassangerId = matchingTicket.Passenger.PassangerId, 
                BookingDate = matchingTicket.BookingDate.ToString(),
                BookingId = matchingTicket.BookingId,
                IsChild=matchingTicket.IsChild,
                Price= matchingTicket.Price,
                CabinClass=matchingTicket.CabinClass,
                FlightId=matchingTicket.FlightId,

            };

            return responseDto;
        }

    }
}
