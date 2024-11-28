using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.User.ClientSide
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "OldPassword")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? OldPassword { get; set; }

        [Display(Name = "NewPassword")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordCompareError)]
        public string ConfirmPassword { get; set; }

    }
}
