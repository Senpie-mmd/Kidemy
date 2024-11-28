using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class ClientSideCourseRequestRegisterViewModel
    {
        public int RequestedById { get; set; }

        [Display(Name = "CourseRequestTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CourseRequestTitle { get; set; }
        public int? PreferedMasterId { get; set; }
        public List<string>? SelectedTags { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Description { get; set; }

    }
}
