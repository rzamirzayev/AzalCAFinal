using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
