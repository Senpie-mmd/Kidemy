using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Models.Blog;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Blog
{
    public class BlogTagRepository : EfRepository<BlogTag, int>, IBlogTagRepository
    {
        public BlogTagRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<List<int>> GetIdsByTitles(List<string> tags)
        {
            return await _dbSet.Where(n => tags.Contains(n.Title)).Select(n => n.Id).ToListAsync();
        }

        public async Task<int> GetTagIdByTitle(string title)
        {
            return await _dbSet.Where(n => n.Title == title).Select(n => n.Id).FirstOrDefaultAsync();
        }

        public async Task<List<BlogTag>> GetTagsThatInsertedInDB(List<string> tagTitles)
        {
            return await _dbSet.Where(n => tagTitles.Contains(n.Title)).ToListAsync();
        }
    }
}
