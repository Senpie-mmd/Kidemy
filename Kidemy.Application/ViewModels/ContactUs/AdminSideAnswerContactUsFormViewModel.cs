using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.ContactUs
{
    public class AdminSideAnswerContactUsFormViewModel : BaseEntityViewModel<int>
    {
        public int ContactUsFormId { get; set; }

        public int AnswererId { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Answer { get; set; }
    }
}
