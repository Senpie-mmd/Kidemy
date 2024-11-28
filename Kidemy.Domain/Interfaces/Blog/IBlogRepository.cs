using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.Blog
{
    public interface IBlogRepository : IRepository<Models.Blog.Blog, int>
    {
        Task<bool> ExistBlogBySlug(string slug);
        Task<bool> ExistBlogByTitle(string title);
        Task<bool> ExistBlogBySlug(string slug, int id);
        Task<bool> ExistBlogByTitle(string title, int id);
        //Task<List<Models.Blog.Blog>> GetAllBlogsForHeader();
        //Task<List<Models.Blog.Blog>> GetAllBlogsForFooter();
        Task<int> GetBlogIdBySlug(string slug);
        Task<Models.Blog.Blog> GetBlogWithTags(int id);
        Task<Models.Blog.Blog> GetPublishedBlogWithTagsAndComments(string slug);
    }
}
