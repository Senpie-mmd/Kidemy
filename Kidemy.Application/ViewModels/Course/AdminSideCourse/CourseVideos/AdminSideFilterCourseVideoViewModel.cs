using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos
{
    public class AdminSideFilterCourseVideoViewModel : BasePaging<AdminSideCourseVideoViewModel>
    {
        public int CourseId { get; set; }
        public string? CourseTitle { get; set; }

        [Display(Name = "CourseVideoTitle")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        [Display(Name = "CourseVideoCategoryTitle")]
        [FilterByLocalizedValue]
        public string? CourseVideoCategoryTitle { get; set; }

        [Display(Name = "IsFree")]
        public bool? IsFree { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }
    }
}
