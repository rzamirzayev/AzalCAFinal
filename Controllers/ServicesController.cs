using Microsoft.AspNetCore.Mvc;

namespace AzalCAFinal.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
