using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class SuggestionCoursesViewComponent : ViewComponent
    {
        #region Fields

        private readonly IDynamicTextService _dynamicTextService;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public SuggestionCoursesViewComponent(ICourseService courseService, IDynamicTextService dynamicTextService)
        {
            _courseService = courseService;
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["DynamicText"] = (await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.TextOfTheProposedCoursesOfHomePage)).Value;

            var result = await _courseService.GetSuggetionCourse(10);

            if (result.IsFailure)
            {
                return View("SuggestionCourses", new List<ClientSideHomePageOfferedCoursesViewModel>());
            }

            return View("SuggestionCourses", result.Value);
        }
    }
}
