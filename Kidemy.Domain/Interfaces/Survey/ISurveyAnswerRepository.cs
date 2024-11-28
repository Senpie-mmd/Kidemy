using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Repository;
using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Models.Survey;

namespace Kidemy.Domain.Interfaces.Survey
{
    public interface ISurveyAnswerRepository : IRepository<Models.Survey.SurveyAnswer, int>
    {
        Task FilterAsync<T>(BasePaging<T> filterModel, FilterConditions<SurveyAnswer> filterConditions, System.Linq.Expressions.Expression<Func<SurveyAnswersReportModel, T>> mapping);
        Task<List<SurveyAnswer>> GetUserAnswersForSurveyIdAsync(int surveyId, int answererId);
    }
}
