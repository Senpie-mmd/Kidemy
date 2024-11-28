using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Survey;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Survey
{
    public class SurveyRepository : EfRepository<Domain.Models.Survey.Survey, int>, ISurveyRepository
    {
        public SurveyRepository(DbContext context) : base(context)
        {
        }

        public Task UnpublishAllPublishedSurveys()
        {
            return _dbSet
                .Where(s => s.IsPublished)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.IsPublished, false)
                                                  .SetProperty(s => s.UpdatedDateOnUtc, DateTime.UtcNow));
        }
    }
}
