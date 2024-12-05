using Microsoft.AspNetCore.Mvc;
using Services.Cities;
using Services.Services;

namespace WebUI.ViewComponents
{
    public class ServicesViewComponent:ViewComponent
    {
        private readonly IServiceService service;

        public ServicesViewComponent(IServiceService
            service)
        {
                this.service = service;
            }
        public async Task<IViewComponentResult> InvokeAsync(string view = null, int? id = null)
        {
            var response = await service.GetById(id.Value);
            if (!string.IsNullOrWhiteSpace(view))
            {
                return View(view, response);
            }
            return View(response);
        }
    }
}
