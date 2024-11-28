using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.DTOs.Course
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string logoFileName { get; set; }
        public string Title { get; set; }
        public int? ParentCourseCategoryId { get; set; }
        public int? CourseCount { get; set; }
    }
}
