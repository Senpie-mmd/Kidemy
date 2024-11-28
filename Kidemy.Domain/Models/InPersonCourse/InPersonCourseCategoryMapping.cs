using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Models.InPersonCourse
{
    public class InPersonCourseCategoryMapping
    {
        public int CourseId { get; set; }

        public int CategoryId { get; set; }

        public InPersonCourse InPersonCourse { get; set; }

        public CourseCategory CourseCategory { get; set; }
    }
}
