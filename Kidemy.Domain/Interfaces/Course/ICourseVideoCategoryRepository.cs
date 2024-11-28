using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseVideoCategoryRepository : IRepository<CourseVideoCategory, int>
    {
        Task<int> GetVideoCategoryIdByTitle(string title);
    }
}
