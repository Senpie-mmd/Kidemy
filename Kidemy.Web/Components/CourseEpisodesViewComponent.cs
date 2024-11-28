using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CourseEpisodesViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public CourseEpisodesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return View("CourseEpisodes", new ClientSideCourseWithVideosViewModel());
            }
            var result = await _courseService.GetCourseVideosByCourseSlug(slug);
            if (result.IsFailure)
            {
                var model = new ClientSideCourseWithVideosViewModel();

                return View("CourseEpisodes", model);
            }

            var userCanPlayAllVideosOfCourse = await _courseService.UserCanPlayAllVideosOfCourse(result.Value.Id, User.GetUserId());

            ViewData["UserCanPlayAllVideosOfCourse"] = userCanPlayAllVideosOfCourse.IsSuccess ? userCanPlayAllVideosOfCourse.Value : false;

            var userHasCourseResult = await _courseService.UserHasCourseAsync(result.Value.Id, User.GetUserId());

            if (userHasCourseResult.IsSuccess)
            {
                ViewData["UserHasCourse"] = userHasCourseResult.Value;
            }

            return View("CourseEpisodes", result.Value);
        }

    }
}
