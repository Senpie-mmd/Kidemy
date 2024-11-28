using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Consultation;

namespace Kidemy.Domain.Models.Consultation
{
    public class ConsultationRequest : AuditBaseEntity<int>
    {
        #region Properties

        public int RequestedByUserId { get; set; }

        public int AdviserId { get; set; }

        public int SelectedDateId { get; set; }

        public int AdviserConsultationTypeId { get; set; }

        public DateTime? FixedDate { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public ConsultationRequestState State { get; set; }

        #endregion

        #region Relations

        public Adviser Adviser { get; set; }
        public AdviserAvailableDate SelectedDate { get; set; }
        public AdviserConsultationType AdviserConsultationType { get; set; }

        #endregion
    }
}
