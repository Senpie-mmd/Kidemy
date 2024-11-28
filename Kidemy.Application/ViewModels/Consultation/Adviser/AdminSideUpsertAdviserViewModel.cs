using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.Adviser
{
    public class AdminSideUpsertAdviserViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int UserId { get; set; }
        [Display(Name = "ConsultationPercentage")]
        [Range(0, 100, ErrorMessage = ErrorMessages.ConsultationPercentageError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int? ConsultationPercentage { get; set; }
        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int? Priority { get; set; }
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
        [Display(Name = "AdviserFullName")]
        public string? AdviserFullName { get; set; }
        [Display(Name = "AdviserConsultationTypes")]

        public List<AdminSideAdviserConsultationTypeViewModel>? AdviserConsultationTypes { get; set; }

        [Display(Name = "AdviserAvailableDates")]

        public List<AdminSideAdviserAvailableDateViewModel>? AdviserAvailableDates { get; set; }
    }
}
