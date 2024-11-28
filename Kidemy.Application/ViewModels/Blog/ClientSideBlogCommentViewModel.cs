using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments
{
    public class ClientSideBlogCommentViewModel : BaseEntityViewModel<int>
    {
        #region Ctor

        public ClientSideBlogCommentViewModel()
        {
            ReplyComments = new List<ClientSideBlogCommentViewModel>();
        }

        #endregion

        #region Properties

        public string UserAvatar { get; set; }
        public string UserName { get; set; }
        public DateTime CommentedDate { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public List<ClientSideBlogCommentViewModel> ReplyComments { get; set; }

        #endregion
    }
}
