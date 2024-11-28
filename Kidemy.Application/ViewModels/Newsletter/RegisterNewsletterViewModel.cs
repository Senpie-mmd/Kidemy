using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Newsletter
{
    public class RegisterNewsletterViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Email")]
        [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Email { get; set; }
    }
}
