﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TicketBookings
{
    public class AddTicketBookingRequestDto
    {
   
        public int PassangerId { get; set; }
        public int FlightId { get; set; }
        public required string CabinClass { get; set; }
        public bool IsChild { get; set; }
        public int Price { get; set; }
        public DateTime BookingDate { get; set; }
        public string TicketNumber { get; set; }
    }
    public class AddTicketBookingResponseDto
    {
        public int BookingId { get; set; }
        public int PassangerId { get; set; }
        public int FlightId { get; set; }
        public required string CabinClass { get; set; }
        public bool IsChild { get; set; }
        public int Price { get; set; }
        public string BookingDate { get; set; }
        public string TicketNumber { get; set; }

    }
}
