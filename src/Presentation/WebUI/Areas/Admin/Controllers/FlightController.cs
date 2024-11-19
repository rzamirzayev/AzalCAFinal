using Microsoft.AspNetCore.Mvc;
using Services.Flight;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService) {
            this.flightService = flightService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await flightService.GetAllAsync();
            return View(data);
        }
    }
}
