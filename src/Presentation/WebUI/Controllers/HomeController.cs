using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using Services;
using Services.Cities;
using Services.Common;
using Services.Flight;
using Services.Implementation;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IContactPostService contactPostService;

        public HomeController(IEmailService emailService,IContactPostService contactPostService)
        {
            this.emailService=emailService;
            this.contactPostService = contactPostService;
        } 
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string fullname,string email,string message)
        {
            var response = contactPostService.Add(fullname, email, message);
            return Json(new
            {
                error = false,
                message = response
            }) ;
        }
    }
}
