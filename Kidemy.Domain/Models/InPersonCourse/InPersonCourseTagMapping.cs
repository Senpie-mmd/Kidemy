namespace Kidemy.Domain.Models.InPersonCourse
{
    public class InPersonCourseTagMapping
    {
        public int CourseId { get; set; }

        public int TagId { get; set; }

        public InPersonCourseTag Tag { get; set; }

        public InPersonCourse Course { get; set; }
    }
}
