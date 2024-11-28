using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Consultation
{
    public class Adviser : AuditBaseEntity<int>
    {
        public int UserId { get; set; }

        public int ConsultationPercentage { get; set; }

        public int Priority { get; set; }

        public bool IsPublished { get; set; }

        public ICollection<AdviserConsultationType> ConsultationTypes { get; set; }
        
        public ICollection<AdviserAvailableDate> AvailableDates { get; set; }
        
        public ICollection<ConsultationRequest> ConsultationRequests { get; set; }
    }
}
