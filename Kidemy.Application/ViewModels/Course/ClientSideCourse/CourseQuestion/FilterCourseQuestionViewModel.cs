using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion
{
    public class FilterCourseQuestionViewModel : BasePaging<ClientSideCourseQuestionViewModel>
    {
        [Display(Name = "Title")]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }
        [Display(Name = "QuestionState")]
        public CourseQuestionState QuestionState { get; set; }
        public int? CourseId { get; set; }
    }
}
