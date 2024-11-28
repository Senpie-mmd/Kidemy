using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageCourseComments")]
    public class CourseCommentController : BaseAdminController
    {
        #region Fields
        private readonly ICourseCommentService _courseCommentService;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public CourseCommentController(ICourseCommentService commentService,
            IStringLocalizer localizer)
        {
            _courseCommentService = commentService;
            _localizer = localizer;
        }
        #endregion

        #region Actions

        [Permission("CourseCommentsList")]
        public async Task<IActionResult> CommentsList(AdminSideFilterCourseCommentsViewModel filter)
        {
            filter.CourseId = 0;

            var result = await _courseCommentService.FilterCommentsAdminSide(filter);
            if (result.IsFailure)
            {
                return View(new AdminSideFilterCourseCommentsViewModel());
            }

            return View(result.Value);
        }

        [Permission("ReportedCommentsList")]
        public async Task<IActionResult> CommentsReports(AdminSideFilterCourseCommentReportViewModel filter)
        {
            var result = await _courseCommentService.FilterCourseCommentReports(filter);
            if (result.IsFailure)
            {
                return View(new AdminSideFilterCourseCommentReportViewModel());
            }

            return View(result.Value);
        }

        [Permission("ConfirmOrDenyComment")]
        public async Task<ResponseResult> ConfirmOrDenyComment(int commentId, bool isConfirm)
        {
            if (commentId < 1) return ResponseResult.Failure(_localizer[ErrorMessages.FailedOperationError].ToString());

            var result = await _courseCommentService.ConfirmOrDenyComment(commentId, isConfirm);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            return ResponseResult.Success();
        }

        [Permission("ReportedCommentsList")]
        public async Task<IActionResult> ViewComment(int commentId, int reportId)
        {
            if (commentId < 1 || reportId < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();

                return RedirectToAction(nameof(CommentsReports));
            }
            var result = await _courseCommentService.MakeCommentReportAsChecked(reportId);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CommentsReports));
            }

            return RedirectToAction(nameof(CommentsList), new { CommentId = commentId });
        }
        #endregion
    }
}
