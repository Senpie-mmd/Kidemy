using Barnamenevisan.Localizing.Attributes;

namespace Kidemy.Application.ViewModels.Survey.SurveyAnswersModels
{
    public class AdminSideSurveyAnswerQuestionViewModel
    {
        public int QuestionId { get; set; }

        [Translate(EntityName = "SurveyQuestion", Key = "Title", PropertyNameOfEntityIdInThisClass = nameof(QuestionId))]
        public string QuestionTitle { get; set; }

        public string AnswerText { get; set; }
    }
}
