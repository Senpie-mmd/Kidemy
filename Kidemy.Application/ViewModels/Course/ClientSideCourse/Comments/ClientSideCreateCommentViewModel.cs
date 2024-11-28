using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments
{
    public class ClientSideCreateCommentViewModel
    {
        [Display(Name = "CommentScore")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public CourseCommentsScore Score { get; set; } = CourseCommentsScore.VeryGood;
        [Display(Name = "Message")]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }
        public string CourseSlug { get; set; }
        public int? ReplyForCommentId { get; set; }
    }
}
