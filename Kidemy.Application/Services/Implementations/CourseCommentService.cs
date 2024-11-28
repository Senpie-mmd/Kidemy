using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseCommentReport;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp;
using System.Web;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseCommentService : ICourseCommentService
    {
        #region Fields
        private readonly ICourseCommentRepository _commentRepository;
        private readonly IUserService _userService;
        private readonly ICourseCommentReportRepository _reportRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor
        public CourseCommentService(ICourseCommentRepository commentRepository,
            IUserService userService,
            ICourseCommentReportRepository reportRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _userService = userService;
            _reportRepository = reportRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods

        public async Task<Result<AdminSideFilterCourseCommentsViewModel>> FilterCommentsAdminSide(AdminSideFilterCourseCommentsViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCourseCommentsViewModel>(ErrorMessages.FailedOperationError);
            var conditions = Filter.GenerateConditions<CourseComment>();

            if (filter.CommentId is not null)
            {
                conditions.Add(n => n.Id == filter.CommentId);
            }

            if (filter.CourseId > 0)
            {
                conditions.Add(n => n.CourseId == filter.CourseId);
            }

            if (filter.UserId is not null)
            {
                conditions.Add(n => n.CommentedById == filter.UserId);
            }

            if (filter.CommentScore is not null)
            {
                conditions.Add(n => n.Score == filter.CommentScore);
            }

            if (filter.IsConfirmed is not null)
            {
                conditions.Add(n => n.IsConfirmed == filter.IsConfirmed);
            }

            if (!string.IsNullOrWhiteSpace(filter.CommentMessage))
            {
                string decodedMessage = HttpUtility.UrlDecode(filter.CommentMessage);
                conditions.Add(n => n.Message.Contains(decodedMessage));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                filter.UserName = HttpUtility.UrlDecode(filter.UserName);
            }

            await _commentRepository.FilterAsync(filter, conditions, CourseCommentMapper.MapCommentsAdminSideViewModel);

            if (filter?.Entities?.Any() ?? false)
            {
                var userIds = filter.Entities.Select(n => n.CommentedById).ToList();
                List<UserFullNameModel> userInfos = await _userService.GetUsersFullNames(userIds);

                foreach (var item in filter.Entities)
                {
                    item.UserName = userInfos.FirstOrDefault(n => n.UserId == item.CommentedById)?.UserFullName;
                }
            }

            return filter;
        }

        public async Task<Result<int>> ConfirmOrDenyComment(int commentId, bool Confirm)
        {
            if (commentId < 1) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment is null) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            if (Confirm == true)
            {
                comment.IsConfirmed = true;
            }
            else
            {
                comment.IsConfirmed = false;
            }

            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync();

            return comment.CourseId;
        }

        public async Task<Result> ReportCommentUserSide(ClientSideReportCommentViewModel model)
        {
            if (!_httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? true) return Result.Failure(ErrorMessages.FailedOperationError);
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!await _commentRepository.AnyAsync(n => n.Id == model.CommentId)) return Result.Failure(ErrorMessages.FailedOperationError);

            CourseCommentReport report = new CourseCommentReport()
            {
                CommentId = model.CommentId,
                Message = model.Message,
                UserId = _httpContextAccessor.HttpContext.User.GetUserId()
            };

            await _reportRepository.InsertAsync(report);
            await _reportRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<AdminSideFilterCourseCommentReportViewModel>> FilterCourseCommentReports(AdminSideFilterCourseCommentReportViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCourseCommentReportViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseCommentReport>();

            if (filter.FromDate is not null)
            {
                conditions.Add(n => n.CreatedDateOnUtc >= filter.FromDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            if (filter.ToDate is not null)
            {
                conditions.Add(n => n.CreatedDateOnUtc < filter.ToDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            if (filter.IsCheckedByAdmin is not null)
            {
                conditions.Add(n => n.IsAdminChecked == filter.IsCheckedByAdmin);
            }

            await _reportRepository.FilterAsync(filter, conditions, CourseCommentMapper.MapReportedCourseComments);

            var userIds = filter.Entities.Select(n => n.ReportedById).ToList();

            if (userIds.Any())
            {
                var userInfos = await _userService.GetUsersFullNames(userIds);
                filter.Entities.Select(n => n.ReportedByFullName = userInfos.FirstOrDefault(info => info.UserId == n.ReportedById)?.UserFullName ?? "-");
            }

            return filter;
        }

        public async Task<Result> MakeCommentReportAsChecked(int reportId)
        {
            if (reportId < 1) return Result.Failure(ErrorMessages.FailedOperationError);

            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report is null) return Result.Failure(ErrorMessages.FailedOperationError);

            report.IsAdminChecked = true;
            report.CheckedById = _httpContextAccessor.HttpContext.User.GetUserId();

            _reportRepository.Update(report);
            await _reportRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result> CreateReplyForComment(ClientSideCreateCommentViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);
            if (model.ReplyForCommentId is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var comment = await _commentRepository.GetByIdAsync((int)model.ReplyForCommentId);
            if (comment is null) return Result.Failure(ErrorMessages.FailedOperationError);

            CourseComment reply = new CourseComment()
            {
                CommentedById = _httpContextAccessor.HttpContext.User.GetUserId(),
                Score = CourseCommentsScore.VeryGood,
                CourseId = comment.CourseId,
                IsConfirmed = false,
                Message = model.Message,
                ReplyCommnetId = comment.Id,
            };

            await _commentRepository.InsertAsync(reply);
            await _commentRepository.SaveAsync();

            return Result.Success();
        }
        #endregion
    }
}
