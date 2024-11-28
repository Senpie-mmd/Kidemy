using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseCommentMapper
    {
        public static Expression<Func<CourseComment, AdminSideCourseCommentViewModel>> MapCommentsAdminSideViewModel => (comment) => new AdminSideCourseCommentViewModel
        {
            Id = comment.Id,
            Score = comment.Score,
            CommentedById = comment.CommentedById,
            IsConfirmed = comment.IsConfirmed,
            Message = comment.Message,
            CommentedDate = comment.CreatedDateOnUtc
        };

        public static Expression<Func<CourseCommentReport, AdminSideComentReportViewModel>> MapReportedCourseComments => (report) => new AdminSideComentReportViewModel
        {
            Id = report.Id,
            Message = report.Message,
            ReportedById = report.UserId,
            ReportedCommentId = report.CommentId,
            IsCheckedByAdmin = report.IsAdminChecked,
            ReportedDate = report.CreatedDateOnUtc
        };
    }
}
