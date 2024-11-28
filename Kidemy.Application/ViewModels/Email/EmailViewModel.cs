using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Email
{
    public class EmailViewModel : BaseEntityViewModel<int>
    {
        #region Properties

        [Display(Name = "From")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        public string From { get; set; }

        [Display(Name = "DisplayName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string DisplayName { get; set; }

        [Display(Name = "SMTP")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Smtp { get; set; }

        [Display(Name = "Port")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int Port { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Password { get; set; }

        [Display(Name = "EnableSsL")]
        public bool EnableSsL { get; set; }

        #endregion        

    }

}
