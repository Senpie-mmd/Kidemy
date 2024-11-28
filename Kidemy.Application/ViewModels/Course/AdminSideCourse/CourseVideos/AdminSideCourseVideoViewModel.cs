using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos
{
    public class AdminSideCourseVideoViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(CourseVideos))]
        public string Title { get; set; }
        public TimeSpan VideoLength { get; set; }
        public int Priority { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
        public int CourseVideoCategoryId { get; set; }

        [Translate(EntityName = nameof(CourseVideoCategory), Key = "Title", PropertyNameOfEntityIdInThisClass = "CourseVideoCategoryId")]
        public string CourseVideoCategoryTitle { get; set; }

        public string? CourseTitle { get; set; }

        public int CourseId { get; set; }   

        public string VideoFileName { get; set; }
        public string? AttachFileName { get; set; }
    }
}
