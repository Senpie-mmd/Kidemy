using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Comment;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Shared;

namespace Kidemy.Domain.Interfaces.Blog
{
    public interface IBlogCommentRepository : IRepository<BlogComment, int>
    {
        Task<List<CommentsCountModel>> GetCommentsCountByBlogIds(List<int> blogIds);
    }
}
