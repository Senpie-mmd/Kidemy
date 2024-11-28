using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Newsletter
{
    public class FilterNewsletterViewModel : BasePaging<AdminSideNewsletterViewModel>
    {
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Ip")]
        public string? Ip { get; set; }
    }
}
