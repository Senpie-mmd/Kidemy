using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseCommentRepository : IRepository<CourseComment, int>
    {
        Task<List<int>> GetAvrageCommentsScore(int courseId);
    }
}
