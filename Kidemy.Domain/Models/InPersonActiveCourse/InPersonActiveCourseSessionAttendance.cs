using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseSessionAttendance : AuditBaseEntity<int>
    {
        public int SessionId { get; set; }

        public int RegisterId { get; set; }

        public string? AttendanceDescription { get; set; }

        public InPersonActiveCourseSession Session { get; set; }
    }
}
