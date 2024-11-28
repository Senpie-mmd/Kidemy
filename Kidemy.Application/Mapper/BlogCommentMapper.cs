using Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs;
using Kidemy.Domain.Models.Blog;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class BlogCommentMapper
    {
        public static Expression<Func<BlogComment, AdminSideBlogCommentViewModel>> MapCommentsAdminSideViewModel => (comment) => new AdminSideBlogCommentViewModel
        {
            Id = comment.Id,
            CommentedById = comment.CommentedById,
            IsConfirmed = comment.IsConfirmed,
            Message = comment.Message,
            CommentedDate = comment.CreatedDateOnUtc,
            BlogId = comment.BlogId,
        };
    }
}
