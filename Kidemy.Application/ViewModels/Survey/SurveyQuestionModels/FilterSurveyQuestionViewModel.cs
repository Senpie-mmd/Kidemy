using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Survey;

namespace Kidemy.Application.ViewModels.Survey.SurveyQuestionModels
{
    public class FilterSurveyQuestionViewModel : BasePaging<AdminSideSurveyQuestionViewModel>
    {
        public SurveyQuestionType? QuestionType { get; set; }

        public int? SurveyId { get; set; }
    }
}
