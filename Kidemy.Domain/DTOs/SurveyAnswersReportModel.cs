namespace Kidemy.Domain.DTOs
{
    public class SurveyAnswersReportModel
    {
        public int SurveyId { get; set; }

        public int AnswererId { get; set; }

        public DateTime CreatedDateOnUtc { get; set; }
    }
}
