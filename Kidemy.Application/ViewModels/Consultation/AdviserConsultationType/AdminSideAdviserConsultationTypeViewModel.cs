using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.AdviserConsultationType
{
    public class AdminSideAdviserConsultationTypeViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "AdviserId")]
        public int? AdviserId { get; set; }
        [Display(Name = "Time(Minute)")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal? Price { get; set; }
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
