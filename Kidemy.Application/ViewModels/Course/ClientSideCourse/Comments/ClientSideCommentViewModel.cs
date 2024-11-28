using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments
{
    public class ClientSideCommentViewModel : BaseEntityViewModel<int>
    {
        public string UserProfile { get; set; }
        public string UserName { get; set; }
        public DateTime CommentedDate { get; set; }
        public string Message { get; set; }
        public CourseCommentsScore Score { get; set; }
        public int UserId { get; set; }
        public int? ReplyForCommentId { get; set; }

        public List<ClientSideCommentViewModel> RepliedComments { get; set; }
    }
}
