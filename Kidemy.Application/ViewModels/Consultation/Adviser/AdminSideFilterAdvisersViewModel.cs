using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.Adviser
{
    public class AdminSideFilterAdvisersViewModel : BasePaging<AdminSideAdviserViewModel>
    {
        [Display(Name = "AdviserUserName")]
        public string AdviserUserName { get; set; }
    }
}
