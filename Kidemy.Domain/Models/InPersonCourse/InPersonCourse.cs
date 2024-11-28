using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.InPersonCourse;

namespace Kidemy.Domain.Models.InPersonCourse
{
    public class InPersonCourse : AuditBaseEntity<int>
    {

        public string Title { get; set; }

        public string Slug { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public string? CourseRequirements { get; set; }

        public string? CourseTopics { get; set; }

        public string? CourseGoals { get; set; }

        public string? CourseAudience { get; set; }

        public string? AttachmentFileName { get; set; }

        public string? MetaTag { get; set; }

        public bool CertificateAvailable { get; set; }

        public bool IsPublished { get; set; }

        public bool PrivateClassAvailable { get; set; }

        public InPersonCourseStatus Status { get; set; }

        public int Priority { get; set; }

        #region Relations

        public ICollection<InPersonCourseCategoryMapping> Categories { get; set; }

        public ICollection<InPersonCourseTagMapping> Tags { get; set; }

        #endregion

    }
}
