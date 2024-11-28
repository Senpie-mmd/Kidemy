using Barnamenevisan.Localizing.Attributes;

namespace Kidemy.Domain.DTOs.Course
{
    public class CourseTitleModel
    {
        public int CourseId { get; set; }

        [Translate(EntityName = "Course", PropertyNameOfEntityIdInThisClass = nameof(CourseId))]
        public string Title { get; set; }
    }
}
