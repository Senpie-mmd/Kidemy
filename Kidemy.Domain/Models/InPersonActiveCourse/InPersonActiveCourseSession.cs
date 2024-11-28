using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.InPersonActiveCourse;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseSession : AuditBaseEntity<int>
    {
        public int ActiveCourseId { get; set; }

        public DateTime SesstionDateOnUtc { get; set; }

        public string Description { get; set; }

        public InPersonSessionStateEnum State { get; set; }

        public string? ReasonOfReject { get; set; }

        public InPersonActiveCourse ActiveCourse { get; set; }

        public ICollection<InPersonActiveCourseSessionFile> SessionFiles { get; set; }

        public ICollection<InPersonActiveCourseSessionAttendance> SessionAttendances { get; set; }
    }
}
