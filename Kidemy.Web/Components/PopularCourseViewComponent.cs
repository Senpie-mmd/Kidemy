using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.DynamicText;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class PopularCourseViewComponent : ViewComponent
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public PopularCourseViewComponent(ICourseService courseService, IDynamicTextService dynamicTextService)
        {
            _courseService = courseService;
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["DynamicText"] = (await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.TextOfTheMostPopularCoursesOfHomePage)).Value;

            var result = await _courseService.GetPopularCourses();

            if (result.IsFailure)
            {
                return View("PopularCourse", new List<ClientSideMainPagePopularCoursesViewModel>());
            }

            return View("PopularCourse", result.Value);
        }
    }
}
