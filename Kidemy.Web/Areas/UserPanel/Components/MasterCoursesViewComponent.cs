using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Components
{
    public class UserPanelMasterCoursesViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Ctor

        public UserPanelMasterCoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _courseService.UserPanelFilterCourses(new FilterUserPanelCoursesViewModel());
            if (result.IsFailure)
            {
                return View("UserPanelMasterCourses", new FilterUserPanelCoursesViewModel());
            }

            return View("UserPanelMasterCourses", result.Value);       
        }
    }

}
