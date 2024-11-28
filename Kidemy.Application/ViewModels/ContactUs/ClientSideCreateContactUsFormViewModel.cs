using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ContactUs
{
    public class ClientSideCreateContactUsFormViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }

        [Display(Name = "FullName")]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        [MaxLength(100, ErrorMessage = ErrorMessages.EmailFormatError)]
        public string Email { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }
    }
}
