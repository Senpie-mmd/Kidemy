using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseTiming : AuditBaseEntity<int>
    {
        public int ActiveCourseId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartDateTimeOnUtc { get; set; }

        public TimeSpan EndDateTimeOnUtc { get; set; }

        public InPersonActiveCourse ActiveCourse { get; set; }
    }
}
