using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.DTOs.Course
{
    public class CourseVideosCategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int VideoCount { get; set; }

        public List<CourseVideo> Videos { get; set; }
    }
}
