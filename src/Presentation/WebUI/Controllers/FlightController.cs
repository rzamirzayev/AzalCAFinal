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
            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;
            return View(availableFlights); 
        }
        public async Task<IActionResult> Detail(int id, int adultCount, int childCount, int infantCount)
        {
            var flight=await flightService.GetById(id);
            ViewBag.AdultCount = adultCount;
            ViewBag.ChildCount = childCount;
            ViewBag.InfantCount = infantCount;
            return View(flight);
        }
    }

}
