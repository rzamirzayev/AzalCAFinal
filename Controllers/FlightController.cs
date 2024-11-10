using Microsoft.AspNetCore.Mvc;

namespace AzalCAFinal.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
