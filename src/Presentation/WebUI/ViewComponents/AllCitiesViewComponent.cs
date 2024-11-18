using Microsoft.AspNetCore.Mvc;
using Services.Cities;

namespace WebUI.ViewComponents
{
    public class AllCitiesViewComponent: ViewComponent
    {
        private readonly ICitiesService citiesService;

        public AllCitiesViewComponent(ICitiesService citiesService) {
            this.citiesService = citiesService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string view = null)
        {
            var response=await citiesService.GetAllAsync();
            if(!string.IsNullOrWhiteSpace(view))
            {
                return View(view,response);
            }
            return View(response);
        } 
    }
}
