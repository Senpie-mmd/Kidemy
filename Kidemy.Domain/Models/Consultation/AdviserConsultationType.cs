using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Consultation
{
    public class AdviserConsultationType : AuditBaseEntity<int>
    {
        public int AdviserId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public bool IsPublished { get; set; }

        public Adviser Adviser { get; set; }
        public ICollection<ConsultationRequest> ConsultationRequest { get; set; }
    }
}
