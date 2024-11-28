using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Survey.SurveyModels
{
    public class AdminSideUpsertSurveyViewModel : LocalizableViewModel<LocalizedAdminSideUpsertSurveyViewModel>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }

    public class LocalizedAdminSideUpsertSurveyViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }
    }
}
