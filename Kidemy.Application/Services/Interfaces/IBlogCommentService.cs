using Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IBlogCommentService
    {
        Task<Result<AdminSideFilterBlogCommentsViewModel>> FilterCommentsAdminSide(AdminSideFilterBlogCommentsViewModel filter);
        Task<Result<int>> ConfirmOrDenyComment(int commentId, bool Confirm);
    }
}
