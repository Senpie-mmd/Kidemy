using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourseSessionFile : AuditBaseEntity<int>
    {
        public int SessionId { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public InPersonActiveCourseSession Session { get; set; }
    }
}
