using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Models.Blog;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Blog
{
    public class BlogRepository : EfRepository<Domain.Models.Blog.Blog, int>, IBlogRepository
    {
        public BlogRepository(KidemyContext context) : base(context)
        {

        }
        public Task<bool> ExistBlogBySlug(string slug)
        {
            return _dbSet.AnyAsync(s => s.IsDeleted == false && s.Slug == slug);
        }

        public Task<bool> ExistBlogByTitle(string title)
        {
            return _dbSet.AnyAsync(s => s.IsDeleted == false && s.Title == title);
        }

        public Task<bool> ExistBlogBySlug(string slug, int id)
        {
            return _dbSet.AnyAsync(s => s.IsDeleted == false && s.Slug == slug && s.Id != id);
        }
        public Task<bool> ExistBlogByTitle(string title, int id)
        {
            return _dbSet.AnyAsync(s => s.IsDeleted == false && s.Title == title && s.Id != id);
        }

        //public Task<List<Domain.Models.Blog.Blog>> GetAllBlogsForHeader()
        //{
        //    return _dbSet.Where(s => s.IsPublished == true && s.LinkPlace == Domain.Enums.Blog.LinkPlace.Header).ToListAsync();
        //}

        //public Task<List<Domain.Models.Blog.Blog>> GetAllBlogsForFooter()
        //{
        //    return _dbSet.Where(s => s.IsPublished == true && s.LinkPlace == Domain.Enums.Blog.LinkPlace.Footer).ToListAsync();
        //}

        public Task<Domain.Models.Blog.Blog> GetBlogWithTags(int id)
        {
            return _dbSet.Include(b => b.BlogTags)
                .ThenInclude(b => b.Tag)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public Task<Domain.Models.Blog.Blog> GetPublishedBlogWithTagsAndComments(string slug)
        {
            return _dbSet.Include(b => b.BlogTags)
                .ThenInclude(b => b.Tag)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished);
        }

        public async Task<int> GetBlogIdBySlug(string slug)
        {
            return (await _dbSet.FirstOrDefaultAsync(b => b.Slug == slug) ?? new Domain.Models.Blog.Blog()).Id;
        }
    }
}
