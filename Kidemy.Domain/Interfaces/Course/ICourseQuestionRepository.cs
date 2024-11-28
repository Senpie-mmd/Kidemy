using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseQuestionRepository : IRepository<CourseQuestion, int>
    {
        Task<List<Models.Course.CourseQuestion>> GetCourseQuestionAfter1MonthAsync();
    }
}
