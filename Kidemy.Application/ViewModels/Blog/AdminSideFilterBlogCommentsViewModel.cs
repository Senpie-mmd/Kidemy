using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs
{
    public class AdminSideFilterBlogCommentsViewModel : BasePaging<AdminSideBlogCommentViewModel>
    {
        public int BlogId { get; set; }

        [Display(Name = "User")]
        public int? UserId { get; set; }

        [Display(Name = "User")]
        public string? UserName { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "Message")]
        public string? CommentMessage { get; set; }
    }
}
