using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Survey;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Survey.SurveyQuestionModels
{
    public class AdminSideUpsertSurveyQuestionViewModel : LocalizableViewModel<LocalizedAdminSideUpsertSurveyQuestionViewModel>
    {
        [Display(Name = "TitleOfQuestion")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }

        public int SurveyId { get; set; }

        [Display(Name = "Options")]
        public string? Options { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        public SurveyQuestionType Type { get; set; }
    }

    public class LocalizedAdminSideUpsertSurveyQuestionViewModel : LocalizedViewModel
    {
        [Display(Name = "TitleOfQuestion")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        [UIHint("textarea")]
        public string? Title { get; set; }

        [Display(Name = "Options")]
        [UIHint("tag")]
        public string? Options { get; set; }
    }
}
