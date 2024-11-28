using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideFilterCourseCommentsViewModel : BasePaging<AdminSideCourseCommentViewModel>
    {
        public int CourseId { get; set; }

        [Display(Name = "User")]
        public int? UserId { get; set; }

        [Display(Name = "User")]
        public string? UserName { get; set; }

        [Display(Name = "CommentScore")]
        public CourseCommentsScore? CommentScore { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "Message")]
        public string? CommentMessage { get; set; }
        public int? CommentId { get; set; }
    }
}
