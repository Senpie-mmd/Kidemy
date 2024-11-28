using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion
{
    public class ClientSideUpsertCourseQuestionViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public bool IsClosed { get; set; }

        public string CourseSlug { get; set; }

    }
}
