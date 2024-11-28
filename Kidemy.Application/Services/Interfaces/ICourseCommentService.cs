using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICourseCommentService
    {
        Task<Result<AdminSideFilterCourseCommentsViewModel>> FilterCommentsAdminSide(AdminSideFilterCourseCommentsViewModel filter);

        Task<Result<int>> ConfirmOrDenyComment(int commentId, bool Confirm);

        Task<Result> ReportCommentUserSide(ClientSideReportCommentViewModel model);

        Task<Result<AdminSideFilterCourseCommentReportViewModel>> FilterCourseCommentReports(AdminSideFilterCourseCommentReportViewModel filter);

        Task<Result> MakeCommentReportAsChecked(int reportId);

        Task<Result> CreateReplyForComment(ClientSideCreateCommentViewModel model);
    }
}
