using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using Services;
using Services.Common;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext db;
        private readonly IEmailService emailService;
        private readonly IContactPostService contactPostService;

        public HomeController(DataContext db,IEmailService emailService,IContactPostService contactPostService)
        {
            this.db = db;
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
