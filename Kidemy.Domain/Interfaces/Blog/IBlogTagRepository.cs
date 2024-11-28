using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Interfaces.Blog
{
    public interface IBlogTagRepository : IRepository<BlogTag, int>
    {
        Task<List<int>> GetIdsByTitles(List<string> tags);

        Task<List<BlogTag>> GetTagsThatInsertedInDB(List<string> tagTitles);

        Task<int> GetTagIdByTitle(string title);
    }
}
