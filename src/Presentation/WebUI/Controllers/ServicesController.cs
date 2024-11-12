using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
