using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseImage : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; }
        public int ActiveCourseId { get; set; }
        public InPersonActiveCourse ActiveCourse { get; set; }
    }
}
