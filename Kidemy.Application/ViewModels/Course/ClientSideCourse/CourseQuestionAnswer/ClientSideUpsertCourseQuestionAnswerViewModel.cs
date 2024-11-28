using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer
{
    public class ClientSideUpsertCourseQuestionAnswerViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Answer")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public int AnsweredById { get; set; }

    }
}
