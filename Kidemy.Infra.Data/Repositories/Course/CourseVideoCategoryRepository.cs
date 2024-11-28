using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseVideoCategoryRepository : EfRepository<CourseVideoCategory, int>, ICourseVideoCategoryRepository
    {
        public CourseVideoCategoryRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<int> GetVideoCategoryIdByTitle(string title)
        {
            return await _dbSet.Where(n => n.Title == title).Select(n => n.Id).FirstOrDefaultAsync();
        }
    }
}
