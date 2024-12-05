using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebUI.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService service;
        public ServicesController(IServiceService service) {
            this.service = service;
        }
        public async Task<IActionResult> Details(int id)
        {
            var response=await service.GetById(id);   
            return View(response);
        }
    }
}
