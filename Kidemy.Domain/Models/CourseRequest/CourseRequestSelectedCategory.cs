using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.CourseRequest
{
    public class CourseRequestSelectedCategory : AuditBaseEntity<int>
    {
        public int CourseRequestId { get; set; }

        public int CategoryId { get; set; }

        public CourseRequest CourseRequest { get; set; }
    }
}
