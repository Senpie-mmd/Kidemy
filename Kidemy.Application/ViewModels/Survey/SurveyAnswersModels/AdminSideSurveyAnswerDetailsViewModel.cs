using Barnamenevisan.Localizing.Attributes;

namespace Kidemy.Application.ViewModels.Survey.SurveyAnswersModels
{
    public class AdminSideSurveyAnswerDetailsViewModel
    {
        public int AnswererId { get; set; }

        public string AnswererFullName { get; set; }

        public int SurveyId { get; set; }

        [Translate(EntityName = "Survey", Key = "Title", PropertyNameOfEntityIdInThisClass = nameof(SurveyId))]
        public string SurveyTitle { get; set; }

        public List<AdminSideSurveyAnswerQuestionViewModel> Answers { get; set; }
    }
}
