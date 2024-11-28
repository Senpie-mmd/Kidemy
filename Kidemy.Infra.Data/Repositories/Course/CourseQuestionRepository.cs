using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseQuestionRepository : EfRepository<CourseQuestion, int>, ICourseQuestionRepository
    {
        public CourseQuestionRepository(DbContext context) : base(context)
        {
        }
        public async Task<List<CourseQuestion>> GetCourseQuestionAfter1MonthAsync()
        {
            var courseQuestions = await _context.Set<Domain.Models.Course.CourseQuestion>()
                .Include(x => x.Answers)
                .Include(x => x.Course)
                .Where(question =>
                question.IsConfirmed && !question.IsClosed
                && !question.IsDeleted
                && question.Answers.OrderBy(z => z.Id).LastOrDefault(answer => answer.AnsweredById == question.AskedById || answer.AnsweredById == question.Course.MasterId).CreatedDateOnUtc.AddDays(30).Date <= DateTime.UtcNow.Date)
                .ToListAsync();

            return courseQuestions;
        }
    }
}
