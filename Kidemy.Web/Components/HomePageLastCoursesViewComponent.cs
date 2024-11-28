using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class HomePageLastCoursesViewComponent : ViewComponent
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public HomePageLastCoursesViewComponent(ICourseService courseService, IDynamicTextService dynamicTextService)
        {
            _courseService = courseService;
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _courseService.GetLastCoursesForHomePage();

            if (result.IsFailure)
            {
                return View("HomePageLastCourses", new List<ClientSideCourseViewModel>());
            }

            return View("HomePageLastCourses", result.Value);
        }
    }
}
