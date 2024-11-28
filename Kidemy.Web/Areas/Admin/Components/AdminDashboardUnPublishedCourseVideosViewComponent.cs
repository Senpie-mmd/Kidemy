using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardUnPublishedCourseVideosViewComponent : ViewComponent
    {
        private readonly ICourseVideoService _courseVideoService;

        public AdminDashboardUnPublishedCourseVideosViewComponent(ICourseVideoService courseVideoService)
        {
            _courseVideoService = courseVideoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AdminSideFilterCourseVideoViewModel()
            {
                IsPublished = false,
                TakeEntity = 5
            };
            var result = await _courseVideoService.FilterCourseVideosForDashboard(model);

            if (result.IsSuccess)
                model = result.Value;

            return View("AdminDashboardUnPublishedCourseVideos", model);
        }
    }
}
