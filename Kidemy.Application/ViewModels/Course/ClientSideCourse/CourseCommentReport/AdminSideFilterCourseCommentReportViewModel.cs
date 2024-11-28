using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport
{
    public class AdminSideFilterCourseCommentReportViewModel : BasePaging<AdminSideComentReportViewModel>
    {
        [Display(Name = "FromDate")]
        public string? FromDate { get; set; }
        [Display(Name = "ToDate")]
        public string? ToDate { get; set; }
        [Display(Name = "CheckedByAdmin")]
        public bool? IsCheckedByAdmin { get; set; }
    }
}
