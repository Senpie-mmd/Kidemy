using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Repository;
using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Interfaces.Survey;
using Kidemy.Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.Survey
{
    public class SurveyAnswerRepository : EfRepository<SurveyAnswer, int>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(DbContext context) : base(context)
        {
        }

        public async Task FilterAsync<T>(
            BasePaging<T> filterModel,
            FilterConditions<SurveyAnswer> filterConditions,
            Expression<Func<SurveyAnswersReportModel, T>> mapping)
        {
            var query = _dbSet.AsQueryable();

            foreach (var condition in filterConditions)
            {
                query = query.Where(condition);
            }

            query = query.OrderByDescending(a => a.CreatedDateOnUtc);

            var groupedQuery = from answer in query
                               group answer by new { answer.AnswererId, answer.Question.SurveyId, answer.CreatedDateOnUtc.Date }
                               into groupedUsers
                               select new SurveyAnswersReportModel
                               {
                                   AnswererId = groupedUsers.Key.AnswererId,
                                   CreatedDateOnUtc = groupedUsers.Key.Date,
                                   SurveyId = groupedUsers.Key.SurveyId,
                               };


            await filterModel.Paging(groupedQuery.Select(mapping));
        }

        public Task<List<SurveyAnswer>> GetUserAnswersForSurveyIdAsync(int surveyId, int answererId)
        {
            return _dbSet.Where(a => a.AnswererId == answererId && a.Question.SurveyId == surveyId)
                         .Include(a => a.Question)
                         .ThenInclude(a => a.Survey)
                         .AsSplitQuery()
                         .ToListAsync();
        }
    }
}
