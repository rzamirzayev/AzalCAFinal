using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Services.Common;
using Services.Flight;
using Services.Passanger;
using Services.TicketBookings;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
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
        public async Task<IActionResult> Index(string departureCity, string destinationCity, string flightDate, string returnFlightDate,int adultCount, int childCount, int infantCount, string tripType)
        {
            //var availableFlights = await flightService.GetAvailableFlights(departureCity, destinationCity, flightDate, adultCount, childCount, infantCount);
            //ViewBag.AdultCount = adultCount;
            //ViewBag.ChildCount = childCount;
            //ViewBag.InfantCount = infantCount;
            //return View(availableFlights);
            if (tripType == "return" && string.IsNullOrEmpty(returnFlightDate))
            {
                ModelState.AddModelError("ReturnFlightDate", "Return flight date is required for round trips.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            FlightSearchViewModel viewModel = new FlightSearchViewModel
            {
                AdultCount = adultCount,
                ChildCount = childCount,
                InfantCount = infantCount,
                TripType = tripType
            };

            if (tripType == "oneway" || string.IsNullOrEmpty(returnFlightDate))
            {
                var availableFlights = await flightService.GetAvailableFlights(
                    departureCity,
                    destinationCity,
                    flightDate,
                    adultCount,
                    childCount,
                    infantCount
                );


                viewModel.OutboundFlights = availableFlights;
                return View(viewModel);
            }

            var availableRoundTripFlights = await flightService.GetAvailableRoundTripFlights(
                departureCity,
                destinationCity,
                flightDate,
                returnFlightDate,
                adultCount,
                childCount,
                infantCount
            );

            viewModel.OutboundFlights = availableRoundTripFlights.OutboundFlights;
            viewModel.ReturnFlights = availableRoundTripFlights.ReturnFlights;

            return View(viewModel);


        }
        public async Task<IActionResult> Detail(int? outboundid,int? returnId, int adultCount, int childCount, int infantCount,string selectedFlightClass,string returnselectedFlightClass)
        {

            if (!outboundid.HasValue)
            {
                return BadRequest("Qalxis flight gelmedi."); 
            }
            var outboundFlight = await flightService.GetById(outboundid.Value);
            if (outboundFlight == null)
            {
                return NotFound("Enis flight gelmedi.");
            }

            var returnFlight = returnId.HasValue
                ? await flightService.GetById(returnId.Value)
                : null;

            ViewBag.OutboundPrice = (selectedFlightClass == "Business")
                ? outboundFlight.BusinessPrice
                : outboundFlight.EconomyPrice;

            if (returnFlight != null)
            {
                ViewBag.ReturnPrice = returnselectedFlightClass == "Economy"
                    ? returnFlight.EconomyPrice
                    : returnFlight.BusinessPrice;
            }

            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;

            FlightDetailsViewModel viewModel = new FlightDetailsViewModel
            {
                OutboundFlights = outboundFlight,
                ReturnFlights = returnFlight
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Passanger(int? id,int? arrivalId, int adultCount, int childCount, int infantCount,string flightClass)
        {
            if (!id.HasValue)
            {
                return BadRequest("Qalxis flight gelmedi.");
            }
            var outboundFlight = await flightService.GetById(id.Value);
            if (outboundFlight == null)
            {
                return NotFound("Enis flight gelmedi.");
            }

            var returnFlight = arrivalId.HasValue
                ? await flightService.GetById(arrivalId.Value)
                : null;

            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;

            if (flightClass == "Economy")
            {
                ViewBag.OutbundPrice = outboundFlight.EconomyPrice;
                if(returnFlight is not null){
                    ViewBag.ReturnPrice = returnFlight.EconomyPrice;


                }
            }
            if (flightClass == "Business")
            {
                ViewBag.Price = outboundFlight.BusinessPrice;

            }
            FlightDetailsViewModel viewModel = new FlightDetailsViewModel
            {
                OutboundFlights = outboundFlight,
                ReturnFlights = returnFlight
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Passanger(List<AddPassangerRequestDto> adult,List<AddPassangerRequestDto> child,List<AddPassangerRequestDto> infant,string email, string phone,int flightId,int adultPrice,int childPrice,int? returnflightId,int returnprice)
        {
            await ProcessPassenger(adult, email, phone, flightId, adultPrice, isChild: false);
            await ProcessPassenger(child, email, phone, flightId, childPrice, isChild: true);
            await ProcessPassenger(infant, email, phone, flightId, childPrice, isChild: true);
            if (returnflightId.HasValue)
            {
                await ProcessPassenger(adult, email, phone, returnflightId.Value, returnprice, isChild: false);
                await ProcessPassenger(child, email, phone, returnflightId.Value, childPrice, isChild: true);
                await ProcessPassenger(infant, email, phone, returnflightId.Value, childPrice, isChild: true);
            }
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

        public async Task<IActionResult> flightByCity(string city,string flightDate,string type)
        {
            var flight=await flightService.GetFlightByCity(city, flightDate, type);
            return View(flight);
        }
        [HttpPost]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { success = false, message = "Email is required." });
                }

                var random = new Random();
                var verificationCode = random.Next(1000, 9999).ToString();

                HttpContext.Session.SetString("VerificationCode", verificationCode);
                HttpContext.Session.SetString("VerificationCodeTimestamp", DateTime.UtcNow.ToString());
                
                await emailService.SendEmail(email, "Your Verification Code", $"Your verification code is: <b>{verificationCode}</b>");

                return Json(new { success = true, message = "Verification code sent successfully to your email." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { success = false, message = "An error occurred on the server.", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult VerifyCode([FromBody] string enteredCode)
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
            <img src='https://www.azal.az/_next/static/media/05112024_Azal_Miles343x128_aze_9607f2801a.221ea906.png' alt='Azerbaijan Airlines' />
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
                <td>{flight.FlightTime}</td>
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
