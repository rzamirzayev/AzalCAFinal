using AzalCAFinal.AppCode.Services;
using AzalCAFinal.Models.Contexts;
using AzalCAFinal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AzalCAFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext db;
        private readonly IEmailService emailService;

        public HomeController(DataContext db,IEmailService emailService)
        {
            this.db = db;
            this.emailService=emailService;
        } 
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(string fullname,string email,string message)
        {
            var post =new ContactPost { FullName=fullname,Email= email, Message = message , CreatedAt=DateTime.Now};
            db.ContactPosts.Add(post);
            db.SaveChanges();
            return Json(new
            {
                error = false,
                message = "muraciet qeyde alindi"
            }) ;
        }
    }
}
