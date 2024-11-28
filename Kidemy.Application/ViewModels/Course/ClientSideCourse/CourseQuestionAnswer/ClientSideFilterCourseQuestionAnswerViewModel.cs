using Barnamenevisan.Localizing.Shared;
using System.Security.Permissions;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer
{
    public class ClientSideFilterCourseQuestionAnswerViewModel : BasePaging<ClientSideCourseQuestionAnswerViewModel>
    {
        public int QuestionId { get; set; }
        public string? CourseSlug { get; set; }
        public string? CourseTitle { get; set; }
        public int AskedById { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsClosed { get; set; }

        public int UserId { get; set; }
    }
}
