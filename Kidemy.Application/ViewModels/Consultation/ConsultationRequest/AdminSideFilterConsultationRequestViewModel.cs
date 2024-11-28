using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Consultation;
using Kidemy.Domain.Events.Consultation.ConsultationRequest;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.ConsultationRequest
{
    public class AdminSideFilterConsultationRequestViewModel : BasePaging<AdminSideConsultaionRequestViewModel>
    {
        public string? RequestedByUserName { get; set; }
        [Display(Name ="State")]
        public FilterConsultationRequestState State { get; set; }
    }
}
