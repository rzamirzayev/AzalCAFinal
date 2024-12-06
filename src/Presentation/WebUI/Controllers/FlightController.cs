using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Common;
using Services.Flight;
using Services.Passanger;
using Services.TicketBookings;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IPassangerService passangerService;
        private readonly ITicketBookingService ticketBookingService;
        private readonly IEmailService emailService;

        public FlightController(IFlightService flightService,IPassangerService passangerService, ITicketBookingService ticketBookingService,IEmailService emailService) {
            this.flightService = flightService;
            this.passangerService=passangerService;
            this.ticketBookingService=ticketBookingService;
            this.emailService = emailService;
        }
   
        [HttpPost]
        public async Task<IActionResult> Index(string departureCity, string destinationCity, string flightDate, int adultCount, int childCount, int infantCount)
        {
            var availableFlights = await flightService.GetAvailableFlights(departureCity, destinationCity, flightDate, adultCount, childCount, infantCount);
            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;
            return View(availableFlights); 
        }
        public async Task<IActionResult> Detail(int id, int adultCount, int childCount, int infantCount,string selectedFlightClass)
        {
            var flight=await flightService.GetById(id);
            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;
            if (selectedFlightClass == "Economy")
            {
                ViewBag.Price = flight.EconomyPrice;
            }
            if (selectedFlightClass == "Business")
            {
                ViewBag.Price = flight.BusinessPrice;

            }
            return View(flight);
        }
        public async Task<IActionResult> Passanger(int id, int adultCount, int childCount, int infantCount,string flightClass)
        {
            var flight = await flightService.GetById(id);
            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;
            if (flightClass == "Economy")
            {
                ViewBag.Price = flight.EconomyPrice;
            }
            if (flightClass == "Business")
            {
                ViewBag.Price = flight.BusinessPrice;

            }
            return View(flight);
        }
        [HttpPost]
        public async Task<IActionResult> Passanger(List<AddPassangerRequestDto> adul,List<AddPassangerRequestDto> child,List<AddPassangerRequestDto> infant,string email, string phone,int flightId,int adultPrice,int childPrice)
        {
            await ProcessPassenger(adul, email, phone, flightId, adultPrice, isChild: false);
            await ProcessPassenger(child, email, phone, flightId, childPrice, isChild: true);
            await ProcessPassenger(infant, email, phone, flightId, childPrice, isChild: true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PassangerGetTicketNumber(string surname, string number)
        {
            var ticketBookingData = await ticketBookingService.GetTicketNumber(surname, number);

            if (ticketBookingData == null)
            {
                return NotFound("Ticket not found.");
            }

            var flight = await flightService.GetById(ticketBookingData.FlightId);

            if (flight == null)
            {
                return NotFound("Flight not found.");
            }

            var passenger = await passangerService.GetById(ticketBookingData.PassangerId);

            if (passenger == null)
            {
                return NotFound("Passenger not found.");
            }

            var viewModel = new PassengerFlightTicketViewModel
            {
                Passenger = passenger,
                Flight = flight,
                TicketBooking = ticketBookingData
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Email is required." });
            }

            
            var random = new Random();
            var verificationCode = random.Next(1000, 9999).ToString();

            HttpContext.Session.SetString("VerificationCode", verificationCode);
            HttpContext.Session.SetString("VerificationCodeTimestamp", DateTime.UtcNow.ToString());

            await emailService.SendEmail(email,"Your Verification Code", $"Your verification code is: <b>{verificationCode}</b>");

            return Json(new { success = true, message = "Verification code sent successfully to your email." });
        }
        [HttpPost]
        public IActionResult VerifyCode(string enteredCode)
        {
            var sessionCode = HttpContext.Session.GetString("VerificationCode");
            var sessionTimestamp = HttpContext.Session.GetString("VerificationCodeTimestamp");

            if (string.IsNullOrEmpty(sessionCode) || string.IsNullOrEmpty(sessionTimestamp))
            {
                return Json(new { success = false, message = "Verification code not found or has expired." });
            }

     
            var codeTimestamp = DateTime.Parse(sessionTimestamp);
            if (DateTime.UtcNow.Subtract(codeTimestamp).TotalMinutes > 1)
            {
                return Json(new { success = false, message = "Verification code has expired." });
            }

            if (sessionCode != enteredCode)
            {
                return Json(new { success = false, message = "Verification code is incorrect." });
            }

            return Json(new { success = true, message = "Verification successful." });
        }


        private string GenerateHtmlBody(AddPassangerRequestDto person, AddTicketBookingRequestDto ticket, AddFlightResponseDto flight, bool isChild, int price)
        {
            string passengerType = isChild ? "CHD" : "ADT";
            return $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #01357e; }}
        .email-container {{ max-width: 600px; margin: auto; background: #fff; border: 1px solid #ddd; padding: 20px; }}
        .header {{ text-align: center; margin-bottom: 20px; }}
        .header img {{ max-width: 200px; }}
        .info-section {{ margin-bottom: 20px; }}
        .info-section h1 {{ font-size: 18px; color: #333; margin-bottom: 5px; }}
        .info-section p {{ margin: 5px 0; font-size: 14px; color: #555; }}
        .itinerary-table {{ width: 100%; border-collapse: collapse; margin-bottom: 20px; }}
        .itinerary-table th, .itinerary-table td {{ border: 1px solid #ddd; padding: 8px; text-align: left; font-size: 14px; }}
        .itinerary-table th {{ background-color: #f7f7f7; font-weight: bold; }}
        .price-section {{ text-align: right; margin-bottom: 20px; }}
        .price-section h2 {{ font-size: 16px; color: #333; margin: 5px 0; }}
        .footer {{ text-align: center; font-size: 12px; color: #777; }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <img src='https://www.azal.az/assets/images/logo.png' alt='Azerbaijan Airlines' />
        </div>
        <div class='info-section'>
            <h1>Electronic Ticket Receipt</h1>
            <p><strong>Passenger:</strong> {person.Name} {person.Surname} ({passengerType})</p>
            <p><strong>Ticket Number:</strong> {ticket.TicketNumber}</p>
        </div>
        <table class='itinerary-table'>
            <tr>
                <th>From</th>
                <th>To</th>
                <th>Flight</th>
                <th>Class</th>
                <th>Date</th>
                <th>Departure</th>
                <th>Arrival</th>
            </tr>
            <tr>
                <td>{flight.DepartureAirport}</td>
                <td>{flight.DestinationAirport}</td>
                <td>{flight.AirplaneName}</td>
                <td>{ticket.CabinClass}</td>
                <td>{flight.DepartureTime}</td>
                <td>{flight.ArrivalTime}</td>
            </tr>
        </table>
        <div class='price-section'>
            <h2>Summary Price</h2>
            <p><strong>Total Amount:</strong> {price} AZN</p>
        </div>
        <div class='footer'>
            Azerbaijan Airlines wishes you a very pleasant trip.<br />
            For assistance, please contact: <a href='mailto:callcenter@azal.az'>callcenter@azal.az</a>
        </div>
    </div>
</body>
</html>";
        }

        private async Task ProcessPassenger(List<AddPassangerRequestDto> passengers, string email, string phone, int flightId, int price, bool isChild)
        {
            var flight = await flightService.GetById(flightId);



            foreach (var person in passengers)
            {
                var passenger = new AddPassangerRequestDto
                {
                    Name = person.Name,
                    Surname = person.Surname,
                    FinCode = person.FinCode,
                    DateOfBirth = person.DateOfBirth,
                    Gender = person.Gender,
                    Phone = phone,
                    Email = email
                };
                var response = await passangerService.AddAsync(passenger);

                string className = (flight.EconomyPrice == price) ? "Economy" : "Business";
                Random random = new Random();
                long min = (long)Math.Pow(10, 12);
                long max = (long)Math.Pow(10, 13) - 1;
                long randomNumber = (long)(min + (max - min) * random.NextDouble());

                var ticket = new AddTicketBookingRequestDto
                {
                    PassangerId = response.PassangerId,
                    FlightId = flightId,
                    CabinClass = className,
                    IsChild = isChild,
                    Price = price,
                    BookingDate = DateTime.Now,
                    TicketNumber = randomNumber.ToString(),
                };

                await ticketBookingService.AddAsync(ticket);

                string htmlBody = GenerateHtmlBody(person, ticket, flight, isChild, price);
                await emailService.SendEmail(email, "Ticket", htmlBody);
            }
        }

       


    }

}
