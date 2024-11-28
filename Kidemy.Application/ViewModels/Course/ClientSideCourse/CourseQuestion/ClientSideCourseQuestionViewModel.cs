using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion
{
    public class ClientSideCourseQuestionViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int AskedById { get; set; }

        public int CourseId { get; set; }

        public bool IsClosed { get; set; }
        public bool IsConfirmed { get; set; }

        public string UserName { get; set; }

        public string UserProfile { get; set; }

        public DateTime CreateDate { get; set; }

        public string CourseTitle { get; set; }
        public string CourseSlug { get; set; }
    }
}
