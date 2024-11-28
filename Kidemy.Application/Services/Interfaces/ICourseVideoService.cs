using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseVideoService
    {
        Task<Result<AdminSideFilterCourseVideoViewModel>> FilterCourseVideosCategory(AdminSideFilterCourseVideoViewModel filter);

        Task<Result<AdminSideFilterCourseVideoViewModel>> FilterCourseVideosForDashboard(AdminSideFilterCourseVideoViewModel filter);

        Task<Result<List<AdminSideCourseVideoCategoryViewModel>>> GetCourseVideoCategoryAsOptions(int courseId);

        Task<Result> CreateNewCourseVideo(AdminSideCreateCourseVideoViewModel model);

        Task<Result> CreateNewCourseVideo(UserPanelCreateNewCourseVideoViewModel model);

        Task<Result<int>> DeleteCourseVideoById(int id);

        Task<Result<AdminSideUpdateCourseVideoViewModel>> GetCourseVideoForEdit(int id);

        Task<Result> UpdateCourseVideo(AdminSideUpdateCourseVideoViewModel model);

        Task<Result<FilterUserPanelCourseVideoViewModel>> UserPanelFilterCourseVideos(FilterUserPanelCourseVideoViewModel filter);

        Task<Result<bool>> PublishCourseVideoAsync(int id);

    }
}
