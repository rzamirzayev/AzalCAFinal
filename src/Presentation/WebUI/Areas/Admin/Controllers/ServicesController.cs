using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly IServiceService service;

        public ServicesController(IServiceService service) {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddServiceRequestDto model)
        {
            await service.AddAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await service.GetById(id);

            if (data == null)
            {
                return NotFound();
            }

            var model = new EditServiceDto
            {
                Id = data.Id,
                Title = data.Title,
                Name = data.Name,
                Description = data.Description
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditServiceDto model)
        {
            await service.EditAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            await service.RemoveAsync(id);
            var result = new { Success = true, Message = "Silindi" };
            return new JsonResult(result);
        }
    }
}
