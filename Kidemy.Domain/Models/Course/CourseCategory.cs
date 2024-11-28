using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Models.InPersonCourse;

namespace Kidemy.Domain.Models.Course
{
    public class CourseCategory : AuditBaseEntity<int>
    {
        public string LogoFileName { get; set; }
        public string Title { get; set; }
        public int? ParentCourseCategoryId { get; set; }
        public bool IsPopular { get; set; }

        #region Relations
        public ICollection<CourseCategory>? SubCategories { get; set; }
        public CourseCategory? ParentCategory { get; set; }
        public ICollection<CourseCategoryMapping>? Courses { get; set; }
        public ICollection<InPersonCourseCategoryMapping>? InPersonCourses { get; set; }
        #endregion
    }
}
