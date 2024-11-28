using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Components
{
    public class MercenaryCoursesViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Ctor

        public MercenaryCoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filter = new ClientSideFilterCourseForUserPanelViewModel();

            filter.UserId = User.GetUserId();
            filter.Type = Domain.Enums.Course.CourseType.Mercenary;
            filter.TakeEntity = 5;

            var result = await _courseService.FilterCoursesOfUser(filter);

            return View("MercenaryCourses", result.Value);
        }
    } 
}
