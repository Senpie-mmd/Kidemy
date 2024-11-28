using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class LastCoursesListViewComponent : ViewComponent
    {
        #region Fields
        private readonly ICourseService _courseService;
        #endregion

        #region Ctor
        public LastCoursesListViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _courseService.GetLastCourses();
            if (result.IsFailure)
            {
                return View("LastCoursesList", new List<ClientSideCourseViewModel>());
            }

            return View("LastCoursesList", result.Value);
        }
    }
}
