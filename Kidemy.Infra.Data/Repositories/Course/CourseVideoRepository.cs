using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseVideoRepository : EfRepository<CourseVideo, int>, ICourseVideoRepository
    {
        public CourseVideoRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<int> PublishCourseVideoAsync(int id)
        {
            return await _dbSet.Where(v => v.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(video => video.IsPublished, true));
        }

    }
}
