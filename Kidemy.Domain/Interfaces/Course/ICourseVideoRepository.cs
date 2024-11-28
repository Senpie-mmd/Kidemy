using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseVideoRepository : IRepository<CourseVideo, int>
    {
        Task<int> PublishCourseVideoAsync(int id);
    }
}
