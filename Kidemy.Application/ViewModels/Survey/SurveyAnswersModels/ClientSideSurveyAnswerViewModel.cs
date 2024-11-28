using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;

namespace Kidemy.Application.ViewModels.Survey.SurveyAnswersModels
{
    public class ClientSideSurveyAnswerViewModel
    {
        public int SurveyId { get; set; }

        public List<ClientSideSurveyQuestionAnswerViewModel> QuestionAnswers { get; set; }
    }
}
