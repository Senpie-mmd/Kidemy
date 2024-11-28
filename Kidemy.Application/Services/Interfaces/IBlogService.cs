using Kidemy.Application.ViewModels.Blog;
using Kidemy.Application.ViewModels.Blog.ClientSideBlog.Comments;
using Kidemy.Domain.Enums.Blog;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Result<FilterBlogViewModel>> FilterAsync(FilterBlogViewModel model);
        Task<Result<ClientSideFilterBlogViewModel>> FilterAsync(ClientSideFilterBlogViewModel model);
        Task<Result<List<ClientSideBlogViewModel>>> GetHomePageBlogs(int count);
        Task<Result<AdminSideUpsertBlogViewModel>> GetBlogForEditByIdAsync(int id);
        Task<Result> CreateAsync(AdminSideUpsertBlogViewModel model);
        Task<Result> UpdateAsync(AdminSideUpsertBlogViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<ClientSideBlogDetailViewModel>> GetBlogDetailBySlug(string slug);
        //Task<Result<List<BlogLinksForClientSideViewModel>>> GetBlogLinks(LinkPlace link);
        Task<Result<ClientSideFilterBlogCommentsViewModel>> GetCommentsForClientSide(string slug);
        Task<Result<string>> CreateComment(ClientSideCreateBlogCommentViewModel model);

    }
}
