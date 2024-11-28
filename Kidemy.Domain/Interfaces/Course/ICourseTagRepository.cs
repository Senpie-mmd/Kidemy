using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseTagRepository : IRepository<CourseTag, int>
    {
        Task<List<int>> GetIdsByTitles(List<string> tags);

        Task<List<CourseTag>> GetTagsThatInsertedInDB(List<string> tagTitles);

        Task<int> GetTagIdByTitle(string title);
    }
}
