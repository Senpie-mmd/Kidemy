using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class LoginViewModel
    {
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [RegularExpression(@"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string Mobile { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
