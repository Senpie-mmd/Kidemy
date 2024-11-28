using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer
{
    public class ClientSideCourseQuestionAnswerViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Answer")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public int AnsweredById { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }

        public string UserName { get; set; }

        public string UserProfile { get; set; }

        public  string QuestionTitle { get; set; }
        public string CourseTitle { get; set; }

        public string CourseSlug { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
