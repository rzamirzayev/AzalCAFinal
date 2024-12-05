using Domain.Entities;
using Services.Flight;
using Services.Passanger;
using Services.TicketBookings;

namespace WebUI.Models
{
        public class PassengerFlightTicketViewModel
        {
            public PassangerGetAllDto Passenger { get; set; }
            public AddFlightResponseDto Flight { get; set; }
            public AddTicketBookingResponseDto TicketBooking { get; set; }
        }

    
}
