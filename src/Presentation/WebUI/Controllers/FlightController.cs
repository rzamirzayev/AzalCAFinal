using Microsoft.AspNetCore.Mvc;
using Services.Flight;

namespace WebUI.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService) {
            this.flightService = flightService;
        }
   
        [HttpPost]
        public async Task<IActionResult> Index(string departureCity, string destinationCity, string flightDate, int adultCount, int childCount, int infantCount)
        {
            var availableFlights = await flightService.GetAvailableFlights(departureCity, destinationCity, flightDate, adultCount, childCount, infantCount);
            return View(availableFlights); 
        }
    }

}
