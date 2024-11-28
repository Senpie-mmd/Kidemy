using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseVideo : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string? ThumbnailImageName { get; set; }
        public string VideoFileName { get; set; }
        public TimeSpan VideoLength { get; set; }
        public int Priority { get; set; }
        public int VideoCategoryId { get; set; }
        public int CourseId { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
        public string? AttachFileName { get; set; }

        #region Relation

        public CourseVideoCategory VideoCategory { get; set; }
        public Course Course { get; set; }

        #endregion
    }
}
