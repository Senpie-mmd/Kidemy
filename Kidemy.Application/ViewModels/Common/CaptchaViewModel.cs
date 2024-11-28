using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Common
{
    public class CaptchaViewModel
    {
        [Display(Name = "Captcha")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Captcha { get; set; }
    }
}
