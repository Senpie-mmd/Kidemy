using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses
{
    public class AdminSideFilterCourseViewModel : BasePaging<AdminSideCourseViewModel>
    {
        [Display(Name = "CourseLevel")]
        public CourseLevel? Level { get; set; }

        [Display(Name = "CoursePriceType")]
        public CourseType? PriceType { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "CourseStatus")]
        public CourseStatus? CourseStatus { get; set; }

    }
}
