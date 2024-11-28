using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CourseMasterViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseMasterViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return View("CourseMaster", new ClientSideMasterDetailsViewModel());
            }
            var result = await _courseService.GetMasterByCourseSlugAsync(slug);

            if (result.IsFailure)
            {
                return View("CourseMaster", new ClientSideMasterDetailsViewModel());
            }

            return View("CourseMaster", result.Value);
        }

    }
}
