using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Survey
{
    public class SurveyAnswer : AuditBaseEntity<int>
    {
        public string AnswerText { get; set; }

        public int AnswererId { get; set; }

        public int QuestionId { get; set; }

        public SurveyQuestion Question { get; set; }
    }

}
