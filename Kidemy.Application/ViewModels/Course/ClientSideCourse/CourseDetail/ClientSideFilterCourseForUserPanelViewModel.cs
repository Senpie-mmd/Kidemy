using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideFilterCourseForUserPanelViewModel : BasePaging<ClientSideCourseForListViewModel>
    {
        public int? UserId { get; set; }

        [Display(Name ="CourseTitle")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        public CourseType? Type { get; set; }
    }
}
