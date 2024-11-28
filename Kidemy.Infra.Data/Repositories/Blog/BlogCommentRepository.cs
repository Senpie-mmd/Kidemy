using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Comment;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Shared;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Blog
{
    public class BlogCommentRepository : EfRepository<BlogComment, int>, IBlogCommentRepository
    {
        public BlogCommentRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<List<CommentsCountModel>> GetCommentsCountByBlogIds(List<int> blogIds)
        {
            var model = await _dbSet
                .Where(x => blogIds.Contains(x.BlogId) && x.IsConfirmed)
                .GroupBy(x => x.BlogId)
                .Select(x => new CommentsCountModel
                {
                    BlogId = x.Key,
                    Count = x.Count()
                }).ToListAsync();

            return model;
        }
    }
}
