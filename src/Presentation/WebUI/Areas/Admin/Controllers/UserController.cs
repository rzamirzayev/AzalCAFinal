using Domain.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Common;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AzalUser> userManager;

        private readonly IEmailService emailService;

        public UserController(UserManager<AzalUser> userManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }
        [Authorize(Policy ="admin.user.get")]
        public async Task<IActionResult> Index()
        {
            var response = await userManager.Users.ToListAsync();
            return View(response);
        }
        [Authorize(Policy = "admin.user.add")]

        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Policy = "admin.user.add")]
        [HttpPost]
        public async Task<IActionResult> Add(string email, string password)
        {
            var user = new AzalUser
            {
                UserName = email,
                Email = email

            };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View();
            }
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = $"{Request.Scheme}://{Request.Host}/approve-account?email={email}&token={token}";

            string message = $"Hesabi tesdiq etmek ucun <a href=\"{link}\">link</a>'le davem edin";

            await emailService.SendEmail(email, "Approve Registration", message);
            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Policy = "admin.user.edit")]

        public async Task<IActionResult> Edit(int id)
        {
            var response = await userManager.Users.FirstOrDefaultAsync(m => m.Id == id);

            return View(response);
        }

        [HttpPost]
        [Authorize(Policy = "admin.user.edit")]
        public async Task<IActionResult> Edit(int id, string UserName)
        {
            var entity = await userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            entity.UserName = UserName;
            await userManager.UpdateAsync(entity);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Policy = "admin.user.remove")]

        public async Task<IActionResult> Remove(int id)
        {
            var entity = await userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            await userManager.DeleteAsync(entity!);
            return RedirectToAction(nameof(Index));
        }


    }
}
