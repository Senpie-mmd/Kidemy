using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.Adviser
{
    public class ClientSideAdviserViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "UserId")]
        public int UserId { get; set; }
        [Display(Name = "ConsultationPercentage")]
        public int ConsultationPercentage { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
        [Display(Name = "AdviserUserName")]
        public string AdviserUserName { get; set; }
        [Display(Name = "AdviserBio")]
        public string? AdviserBio { get; set; }

        public string AdviserProfile { get; set; }

        [Display(Name = "AdviserConsultationTypes")]

        public List<ClientSideAdviserConsultationTypeViewModel>? AdviserConsultationTypes { get; set; }

        [Display(Name = "AdviserAvailableDates")]

        public List<ClientSideAdviserAvailableDateViewModel>? AdviserAvailableDates { get; set; }

    }
}
