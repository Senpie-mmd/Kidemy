using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [RegularExpression(@"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string Mobile { get; set; }

        [Display(Name = "ConfirmationCode")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string ConfirmationCode { get; set; }
    }
}
