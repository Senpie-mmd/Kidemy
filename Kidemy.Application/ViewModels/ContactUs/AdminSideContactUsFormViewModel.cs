using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.ContactUs;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ContactUs
{
    public class AdminSideContactUsFormViewModel : BaseEntityViewModel<int>
    {
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

        [Display(Name = "State")]
        public ContactUsFormState State { get; set; }

        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        public int? AnswererId { get; set; }

        public int? ParentId { get; set; }

        public string? AnswerText { get; set; }

        public int UserId { get; set; }
    }
}
