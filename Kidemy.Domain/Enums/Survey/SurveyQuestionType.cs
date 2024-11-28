using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Survey
{
    public enum SurveyQuestionType
    {
        [Display(Name = "Descriptive")]
        Descriptive,
        [Display(Name = "MultipleChoice")]
        MultipleChoice,
        [Display(Name = "OneChoice")]
        OneChoice
    }
}
