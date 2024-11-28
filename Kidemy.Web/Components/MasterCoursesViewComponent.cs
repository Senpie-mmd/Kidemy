using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class MasterCoursesViewComponent : ViewComponent
    {

        #region Fields

        private readonly ICourseService _courseService;

        #endregion

        #region Ctor

        public MasterCoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int masterId)
        {

            var courses = await _courseService.FilterCoursesForClientSide(new ClientSideFilterCoursesViewModel { MasterId=masterId});

            return View("MasterCourses", courses.Value);
        }
    }
}
