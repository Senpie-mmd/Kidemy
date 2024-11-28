using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.CourseRequest
{
    public class CourseRequestVote : AuditBaseEntity<int>
    {
        public int CourseRequestId { get; set; }

        public int UserId { get; set; }

        public bool IsAgree { get; set; }
    }
}
