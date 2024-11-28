using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ConfirmMobileViewModel
    {
        [Display(Name = "ConfirmationCode")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(8, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string ConfirmationCode { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [RegularExpression(@"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string Mobile { get; set; }
    }
}
