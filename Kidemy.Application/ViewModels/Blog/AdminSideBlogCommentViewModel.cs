using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs
{
    public class AdminSideBlogCommentViewModel : BaseEntityViewModel<int>
    {
        public DateTime CommentedDate { get; set; }
        public int CommentedById { get; set; }
        public string? UserName { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public int BlogId { get; set; }
    }
}
