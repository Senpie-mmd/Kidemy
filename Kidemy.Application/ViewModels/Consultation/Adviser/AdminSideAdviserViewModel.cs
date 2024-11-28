using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.Consultation.Adviser
{
    public class AdminSideAdviserViewModel : BaseEntityViewModel<int>
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

        [Display(Name = "AdviserConsultationTypes")]
        public List<AdminSideAdviserConsultationTypeViewModel> AdviserConsultationTypes { get; set; }

        [Display(Name = "AdviserAvailableDates")]
        public List<AdminSideAdviserAvailableDateViewModel> AdviserAvailableDates { get; set; }
    }
}
