using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport
{
    public class AdminSideComentReportViewModel : BaseEntityViewModel<int>
    {
        public int ReportedById { get; set; }
        public string ReportedByFullName { get; set; }
        public string Message { get; set; }
        public int ReportedCommentId { get; set; }
        public bool IsCheckedByAdmin { get; set; }
        public DateTime ReportedDate { get; set; }
    }
}
