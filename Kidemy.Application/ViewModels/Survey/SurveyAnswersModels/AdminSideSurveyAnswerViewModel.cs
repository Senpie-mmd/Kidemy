namespace Kidemy.Application.ViewModels.Survey.SurveyAnswersModels
{
    public class AdminSideSurveyAnswerViewModel
    {
        public int SurveyId { get; set; }

        public int AnswererId { get; set; }

        public string AnswererFullName { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
