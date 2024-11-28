using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Domain.Events.VIPPlan;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseVideoMapper
    {
        public static Expression<Func<CourseVideo, AdminSideCourseVideoViewModel>> MapCourseVideos => (video) => new AdminSideCourseVideoViewModel
        {
            Id = video.Id,
            Title = video.Title,
            CourseVideoCategoryId = video.VideoCategoryId,
            CourseVideoCategoryTitle = video.VideoCategory.Title,
            IsFree = video.IsFree,
            IsPublished = video.IsPublished,
            Priority = video.Priority,
            VideoLength = video.VideoLength,
            VideoFileName = video.VideoFileName,
            CourseId = video.CourseId,
            CourseTitle = video.Course.Title,
            AttachFileName = video.AttachFileName
        };

        public static AdminSideCourseVideoCategoryViewModel MapFrom(this AdminSideCourseVideoCategoryViewModel model, CourseVideoCategory category)
        {
            model.Id = category.Id;
            model.Title = category.Title;

            return model;
        }

        public static AdminSideUpdateCourseVideoViewModel MapFrom(this AdminSideUpdateCourseVideoViewModel model, CourseVideo video)
        {
            model.Id = video.Id;
            model.Title = video.Title;
            model.CourseVideoCategoryTitle = video.VideoCategory.Title;
            //model.VideoThumbnailImageFileName = video.ThumbnailImageName;
            model.VideoFileName = video.VideoFileName;
            model.VideoLength = video.VideoLength.ToString();
            model.Priority = video.Priority;
            model.IsFree = video.IsFree;
            model.IsPublished = video.IsPublished;
            model.CourseId = video.CourseId;
            model.AttachFileName = video.AttachFileName;

            return model;
        }

        public static Expression<Func<CourseVideo, UserPanelCourseVideoViewModel>> MapCourseVideosForMaster => (video) => new UserPanelCourseVideoViewModel
        {
            Id = video.Id,
            IsFree = video.IsFree,
            IsPublished = video.IsPublished,
            Title = video.Title,
            VideoLength = video.VideoLength,

        };
    }
}
