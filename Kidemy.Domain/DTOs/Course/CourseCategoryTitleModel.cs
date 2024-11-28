using Barnamenevisan.Localizing.Attributes;

namespace Kidemy.Domain.DTOs.Course
{
    public class CourseCategoryTitleModel
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }

        [Translate(EntityName = "CourseCategory", PropertyNameOfEntityIdInThisClass = nameof(CategoryId))]
        public string Title { get; set; }
    }
}
