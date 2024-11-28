using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;

namespace Kidemy.Application.ViewModels.Blog.ClientSideBlog.Comments
{
    public class ClientSideFilterBlogCommentsViewModel : BasePaging<ClientSideBlogCommentViewModel>
    {
        public string BlogSlug { get; set; }
    }
}
