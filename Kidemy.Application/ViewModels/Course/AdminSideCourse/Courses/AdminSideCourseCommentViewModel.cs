using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideCourseCommentViewModel : BaseEntityViewModel<int>
    {
        public DateTime CommentedDate { get; set; }
        public int CommentedById { get; set; }
        public string? UserName { get; set; }
        public CourseCommentsScore Score { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
