using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseVideo : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string VideoFile { get; set; }
        public int ActiveCourseId { get; set; }
        public InPersonActiveCourse ActiveCourse { get; set; }
    }
}
