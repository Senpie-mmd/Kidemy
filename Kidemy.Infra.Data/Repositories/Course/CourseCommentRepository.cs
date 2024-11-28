using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseCommentRepository : EfRepository<CourseComment, int>, ICourseCommentRepository
    {
        public CourseCommentRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<List<int>> GetAvrageCommentsScore(int courseId)
        {
            return await _dbSet.Where(n => n.CourseId == courseId && n.IsConfirmed == true && n.ReplyCommnetId == null).Select(n => (int)n.Score).ToListAsync();
        }
    }
}
