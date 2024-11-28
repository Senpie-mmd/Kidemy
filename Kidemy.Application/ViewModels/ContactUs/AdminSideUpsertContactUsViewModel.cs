using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ContactUs
{
    public class AdminSideUpsertContactUsViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Address")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Address { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [RegularExpression(@"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        public string Email { get; set; }
    }
}
