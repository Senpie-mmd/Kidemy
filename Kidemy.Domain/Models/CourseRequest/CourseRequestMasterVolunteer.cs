using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.CourseRequest
{
    public class CourseRequestMasterVolunteer : AuditBaseEntity<int>
    {
        public int CourseRequestId { get; set; }

        public int MasterId { get; set; }

        public string MasterDescription { get; set; }

        public string? AdminDescription { get; set; }

        public CourseRequest CourseRequest { get; set; }
    }
}
