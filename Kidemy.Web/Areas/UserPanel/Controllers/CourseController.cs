using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class CourseController : BaseUserPanelController
    {
        #region Fields

        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Ctor

        public CourseController(ICourseService courseService, IStringLocalizer<SharedResource> localizer)
        {
            _courseService = courseService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [HttpGet("/userpanel/free-courses")]
        public async Task<IActionResult> FreeCoursesList()
        {
            var filter = new ClientSideFilterCourseForUserPanelViewModel();

            filter.UserId = User.GetUserId();
            filter.Type = Domain.Enums.Course.CourseType.Free;

            var result = await _courseService.FilterCoursesOfUser(filter);

            return View(result.Value);
        }

        [HttpPost("/userpanel/free-courses")]
        public async Task<IActionResult> FreeCoursesList(ClientSideFilterCourseForUserPanelViewModel filter)
        {
            filter.UserId = User.GetUserId();
            filter.Type = Domain.Enums.Course.CourseType.Free;

            var result = await _courseService.FilterCoursesOfUser(filter);

            return View(result.Value);
        }

        [HttpGet("/userpanel/my-courses")]
        public async Task<IActionResult> MercenaryCoursesList()
        {
            var filter  = new ClientSideFilterCourseForUserPanelViewModel();

            filter.UserId = User.GetUserId();
            filter.Type = Domain.Enums.Course.CourseType.Mercenary;

            var result = await _courseService.FilterCoursesOfUser(filter);

            return View(result.Value);
        }

        [HttpPost("/userpanel/my-courses")]
        public async Task<IActionResult> MercenaryCoursesList(ClientSideFilterCourseForUserPanelViewModel filter)
        {
            filter.UserId = User.GetUserId();
            filter.Type = Domain.Enums.Course.CourseType.Mercenary;

            var result = await _courseService.FilterCoursesOfUser(filter);

            return View(result.Value);
        }

        #endregion
    }
}
