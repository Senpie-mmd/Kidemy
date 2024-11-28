using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion
{
    public class AdminSideCourseQuestionViewModel : BaseEntityViewModel<int>
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

        public string MasterFirstName { get; set; }
        public string MasterLastName { get; set; }
        public int MasterId { get; set; }
    }
}
