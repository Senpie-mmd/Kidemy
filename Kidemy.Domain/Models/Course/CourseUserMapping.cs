namespace Kidemy.Domain.Models.Course
{
    public class CourseUserMapping
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }

        public Course Course { get; set; }
    }
}
