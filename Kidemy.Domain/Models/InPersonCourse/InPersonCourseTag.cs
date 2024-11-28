using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.InPersonCourse
{
    public class InPersonCourseTag : AuditBaseEntity<int>
    {
        public string Title { get; set; }

        public ICollection<InPersonCourseTagMapping> Courses { get; set; }
    }
}
