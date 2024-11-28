using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Survey;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Survey
{
    public class SurveyQuestionRepository : EfRepository<Domain.Models.Survey.SurveyQuestion, int>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(DbContext context) : base(context)
        {
        }
    }
}
