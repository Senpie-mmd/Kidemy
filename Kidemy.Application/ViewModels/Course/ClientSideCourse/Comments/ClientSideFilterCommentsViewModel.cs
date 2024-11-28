using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments
{
    public class ClientSideFilterCommentsViewModel : BasePaging<ClientSideCommentViewModel>
    {
        public string CourseSlug { get; set; }
        public double AvrageCourseScore { get; set; } = 0;
    }
}
