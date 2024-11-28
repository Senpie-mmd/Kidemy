using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Consultation;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.ConsultationRequest
{
    public class UpsertConsultationRequestViewModel : BaseEntityViewModel<int>
    {
        public int? RequestedByUserId { get; set; }

        public int? AdviserId { get; set; }

        public int? SelectedDateId { get; set; }

        public int? AdviserConsultationTypeId { get; set; }

        public DateTime? FixedDate { get; set; }

        [Display(Name = "Topic")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Topic { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }

        public ConsultationRequestState State { get; set; }
    }
}
