using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideCourseViewModel : BaseEntityViewModel<int>
    {
        public string Title { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public int MasterId { get; set; }
        public decimal? Price { get; set; } = 0;
        public string Slug { get; set; }
        public CourseType CourseType { get; set; }
    }
}
