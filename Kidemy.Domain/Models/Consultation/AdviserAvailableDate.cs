using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Consultation
{
    public class AdviserAvailableDate : BaseEntity<int>
    {
        public int AdviserId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public Adviser Adviser { get; set; }
        public ICollection<ConsultationRequest> ConsultationRequest { get; set; }
    }
}
