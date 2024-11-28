using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "NewPassword")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Password { get; set; }

        [Display(Name = "ConfirmNewPassword")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordCompareError)]
        public string ConfirmPassword { get; set; }
    }
}
