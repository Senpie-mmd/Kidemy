using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Survey
{
    public interface ISurveyRepository : IRepository<Models.Survey.Survey, int>
    {
        Task UnpublishAllPublishedSurveys();
    }
}
