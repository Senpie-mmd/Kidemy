using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    [Permission("Master")]
    public class MangeMasterCoursesController : BaseUserPanelController
    {
        #region Fields
        private readonly ICourseService _courseServiec;
        private readonly IStringLocalizer _localizer;
        private readonly ICourseVideoService _videoService;
        #endregion

        #region Ctor
        public MangeMasterCoursesController(ICourseService courseService,
            IStringLocalizer localizer,
            ICourseVideoService videoService)
        {
            _courseServiec = courseService;
            _localizer = localizer;
            _videoService = videoService;
        }
        #endregion

        #region Actions

        [Permission("Master")]
        [HttpGet("/userpanel/manage-courses")]
        public async Task<IActionResult> MasterCoursesList(FilterUserPanelCoursesViewModel filter)
        {
            var result = await _courseServiec.UserPanelFilterCourses(filter);
            if (result.IsFailure)
            {
                return View(new FilterUserPanelCoursesViewModel());
            }
            return View(filter);
        }

        [Permission("Master")]
        [HttpGet("/userpanel/course-episodes")]
        public async Task<IActionResult> CourseEpisodes(FilterUserPanelCourseVideoViewModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.slug))
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(MasterCoursesList));
            }
            var result = await _videoService.UserPanelFilterCourseVideos(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(new FilterUserPanelCourseVideoViewModel());
            }
            return View(result.Value);
        }

        [Permission("Master")]
        [HttpGet("/userpanel/new-episode")]
        public async Task<IActionResult> CreateNewCourseEpisode(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(MasterCoursesList));
            }

            var courseId = await _courseServiec.GetCourseIdBySlug(slug);
            if (courseId < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(MasterCoursesList));
            }

            var CanChangeIsFreeResult = await _courseServiec.CheckIfCourseVideoCanBeFree(courseId);

            var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(courseId);

            if (videoCategoryResult.IsFailure)
            {
                return View(new UserPanelCreateNewCourseVideoViewModel() { CourseId = courseId, CanChangeIsFree = CanChangeIsFreeResult.Value });
            }
            var model = new UserPanelCreateNewCourseVideoViewModel() { VideoCategories = videoCategoryResult.Value, CourseId = courseId, CanChangeIsFree = CanChangeIsFreeResult.Value };
            return View(model);
        }

        [Permission("Master")]
        [HttpPost("/userpanel/new-episode")]
        public async Task<IActionResult> CreateNewCourseEpisode(UserPanelCreateNewCourseVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);

                if (videoCategoryResult.IsFailure)
                {
                    return View(model);
                }
                model.VideoCategories = videoCategoryResult.Value;

                return View(model);
            }
            var result = await _videoService.CreateNewCourseVideo(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();

                var videoCategoryResult = await _videoService.GetCourseVideoCategoryAsOptions(model.CourseId);

                if (videoCategoryResult.IsFailure)
                {
                    return View(model);
                }
                model.VideoCategories = videoCategoryResult.Value;
                return View(model);
            }
            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            var slugResult = await _courseServiec.GetCourseSlugById(model.CourseId);

            return RedirectToAction(nameof(CourseEpisodes), new { slug = slugResult.Value });
        }

        #endregion
    }
}