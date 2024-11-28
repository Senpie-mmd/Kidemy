using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonCourse
{
    public class InPersonCourseVisit : AuditBaseEntity<int>
    {
        public int CourseId { get; set; }

        public int? UserId { get; set; }

        public string? IP { get; set; }
    }
}
