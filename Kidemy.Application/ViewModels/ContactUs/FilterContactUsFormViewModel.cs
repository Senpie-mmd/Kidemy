using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.ContactUs;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ContactUs
{
    public class FilterContactUsFormViewModel : BasePaging<AdminSideContactUsFormViewModel>
    {
        [Display(Name = "State")]
        public ContactUsStateForFilter State { get; set; }
    }
}
