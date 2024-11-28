using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Survey;

namespace Kidemy.Domain.Models.Survey
{
    public class SurveyQuestion : AuditBaseEntity<int>
    {
        public string Title { get; set; }

        public SurveyQuestionType Type { get; set; }

        public int SurveyId { get; set; }

        /// <summary>
        /// separated with ","
        /// </summary>
        public string? Options { get; set; }

        public int Priority { get; set; }

        public Survey Survey { get; set; }

        public ICollection<SurveyAnswer> Answers { get; set; }
    }
}
