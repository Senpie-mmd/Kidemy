using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideCategoriesForCourseListViewModel : BaseEntity<int>
    {
        [Translate(EntityName = nameof(Course))]
        public string Title { get; set; }
        public int CourseCount { get; set; }
        public int? ParentId { get; set; }
    }
}
