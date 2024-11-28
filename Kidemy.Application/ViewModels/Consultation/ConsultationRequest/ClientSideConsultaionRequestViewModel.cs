using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using Kidemy.Domain.Enums.Consultation;

namespace Kidemy.Application.ViewModels.Consultation.ConsultationRequest
{
    public class ClientSideConsultaionRequestViewModel : BaseEntityViewModel<int>
    {
        public string AdviserUserName { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }

        public DateTime? FixedTime { get; set; }

        public ConsultationRequestState state { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string TypeTitle { get; set; }
        public decimal TypePrice { get; set; }
        public int? AdviserUserId { get; set; }

        public ClientSideAdviserAvailableDateViewModel  Date { get; set; }
        public ClientSideAdviserConsultationTypeViewModel  Type { get; set; }
    }
}
