using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class OfferdCoursesListViewComponent : ViewComponent
    {
        #region Fields
        private readonly ICourseService _courseServide;
        #endregion

        #region Ctor
        public OfferdCoursesListViewComponent(ICourseService courseService)
        {
            _courseServide = courseService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int MasterId, int CurrentCourseId)
        {
            if (MasterId < 1)
                return View("OfferdCourses", new List<ClientSideMastersOtherCoursesViewModel>());

            var result = await _courseServide.GetOfferedCoursesForMaster(MasterId, CurrentCourseId);
            if (result.IsFailure)
            {
                return View("OfferdCourses", new List<ClientSideMastersOtherCoursesViewModel>());
            }

            return View("OfferdCourses", result.Value);
        }
    }
}
