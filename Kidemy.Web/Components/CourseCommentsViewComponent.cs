using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class CourseCommentsViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseCommentsViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string slug, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return View("CourseComments", new ClientSideFilterCommentsViewModel());
            }

            var allowCommentingResult = await _courseService.IsCommentsOpen(slug);
            if (allowCommentingResult.IsFailure)
            {
                return View("NotAllowedCommenting");
            }

            if (allowCommentingResult.Value == false)
                return View("NotAllowedCommenting");

            var result = await _courseService.GetCommentsForClientSide(slug, page);
            if (result.IsFailure)
            {
                return View("CourseComments", new ClientSideFilterCommentsViewModel());
            }

            if (!User?.Identity?.IsAuthenticated ?? true)
            {
                TempData["returnUrl"] = Url.Action("CourseDetails", new { slug = slug });
            }

            return View("CourseComments", result.Value);
        }

    }
}
