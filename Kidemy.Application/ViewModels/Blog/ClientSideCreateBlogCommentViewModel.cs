using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Blog.ClientSideBlog.Comments
{
    public class ClientSideCreateBlogCommentViewModel
    {
        [Display(Name = "CommentText")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CommentText { get; set; }

        public string BlogSlug { get; set; }

        public int? ReplyCommentId { get; set; }
    }
}
