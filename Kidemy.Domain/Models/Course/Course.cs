using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Domain.Models.Course
{
    public class Course : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public bool IsOffer { get; set; }
        public int MasterId { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan VideosTotalTime { get; set; }
        public bool HasCertificate { get; set; }
        public bool AllowComenting { get; set; }
        public CourseType Type { get; set; }
        public bool IsPublished { get; set; }
        public int MasterCommissionPercentage { get; set; }
        public string FileSuffix { get; set; }
        public string ImageName { get; set; }
        public string? DemoVideoFileName { get; set; }

        #region Relation
        public ICollection<CourseCategoryMapping> Categories { get; set; }
        public ICollection<CourseQuestion> CourseQuestions { get; set; }
        public ICollection<CourseTagMapping> CourseTags { get; set; }
        public ICollection<CourseVideo> Videos { get; set; }
        public ICollection<CourseComment> Comments { get; set; }

        public ICollection<CourseView> CourseViews { get; set; }
        public ICollection<CourseUserMapping> Users { get; set; }
        #endregion
    }

}
