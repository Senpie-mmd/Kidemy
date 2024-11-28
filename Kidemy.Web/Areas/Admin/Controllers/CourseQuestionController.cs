using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestionAnswer;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageCourseQuestion")]
    public class CourseQuestionController : BaseAdminController
    {
        #region Fields
        private readonly ICourseQuestionService _courseQuestionService;
        private readonly ICourseQuestionAnswerService _courseQuestionAnswer;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor
        public CourseQuestionController(ICourseQuestionService courseQuestionService, IStringLocalizer<SharedResource> localizer, ICourseQuestionAnswerService courseQuestionAnswer)
        {
            _courseQuestionService = courseQuestionService;
            _localizer = localizer;
            _courseQuestionAnswer = courseQuestionAnswer;
        }
        #endregion

        #region Actions

        #region List

        [Permission("CourseQuestionList")]
        [HttpGet]
        public async Task<IActionResult> List(AdminSideFilterCourseQuestionViewModel filter)
        {
            var result = await _courseQuestionService.FilterAsync(filter);

            return View(result.Value);
        }
        [Permission("NotConfirmedCourseQuestionAndAnswersList")]
        public async Task<IActionResult> NotConfirmedCourseQuestionAndAnswersList(AdminSideFilterCourseQuestionViewModel model)
        {
            var models = await _courseQuestionService.FilterNotConfirmedAsync(model);

            return View(models.Value);
        }

        [Permission("CourseQuestionsDoesNotAnsweredByTeacherAfter48HoursList")]
        public async Task<IActionResult> CourseQuestionsDoesNotAnsweredByTeacherAfter48Hours(AdminSideFilterCourseQuestionViewModel filter)
        {
            var result = await _courseQuestionService.FilterAdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursAsync(filter);

            return View(result.Value);
        }

        #endregion

        #region MessagesList

        [Permission("CourseQuestionMessagesList")]
        [HttpGet]
        public async Task<IActionResult> ShowMessages(AdminSideFilterCourseQuestionAnswerViewModel model)
        {
            var answers = await _courseQuestionAnswer.GetCourseQuestionAnswersByQuestionIdForAdminAsync(model.QuestionId);

            var question = await _courseQuestionService.GetCourseQuestionsById(model.QuestionId);

            if (question.IsSuccess)
                ViewBag.Question = question.Value;

            return View(answers.Value);
        }

        #endregion

        #region OpenAndCloseQuestion
        [Permission("CloseCourseQuestion")]
        public async Task<ResponseResult> CloseQuestion(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionService.CloseCourseQuestionAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }
        [Permission("OpenCourseQuestion")]
        public async Task<ResponseResult> OpenQuestion(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionService.OpenCourseQuestionAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }
        #endregion

        #region ConfrimCourseQuestion

        [Permission("ConfirmCourseQuestion")]
        public async Task<ResponseResult> ConfrimCourseQuestion(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionService.ConfrimCourseQuestion(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region Delete
        [Permission("DeleteCourseQuestion")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region Course Question Answer

        #region Delete Answer
        [Permission("DeleteCourseQuestionAnswer")]
        public async Task<ResponseResult> DeleteQuestionAnswer(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionAnswer.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }
        #endregion

        #region Confirm Answer
        [Permission("ConfirmCourseQuestionAnswer")]
        public async Task<ResponseResult> ConfrimCourseQuestionAnswer(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _courseQuestionAnswer.ConfirmAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #endregion

        #endregion
    }
}
