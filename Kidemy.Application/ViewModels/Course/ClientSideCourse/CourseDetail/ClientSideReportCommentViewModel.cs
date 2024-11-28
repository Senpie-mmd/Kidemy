using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideReportCommentViewModel
    {
        public int CommentId { get; set; }

        [Display(Name = "ReportMessage")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Message { get; set; }
    }
}
