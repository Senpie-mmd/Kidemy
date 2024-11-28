using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.ConsultationRequest
{
    public class AdminSideSetTimeForConsultationRequestViewModel : BaseEntityViewModel<int>
    {
        public int ConsultationRequestId { get; set; }
        [Display(Name = "FixedDate")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string SetFixedDate { get; set; }
        [Display(Name = "FixedTime")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public TimeSpan SetFixedTime { get; set; }
    }
}
