using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Survey
{
    public class Survey : AuditBaseEntity<int>
    {
        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public ICollection<SurveyQuestion> Questions { get; set; }
    }
}
