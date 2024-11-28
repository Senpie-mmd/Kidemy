using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Survey.SurveyAnswersModels;
using Kidemy.Application.ViewModels.Survey.SurveyModels;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageSurvey")]
    public class SurveyController : BaseAdminController
    {
        #region Fields

        private readonly ISurveyService _surveyService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public SurveyController(ISurveyService surveyService, IStringLocalizer<SharedResource> localizer)
        {
            _surveyService = surveyService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region Survey

        #region List

        [Permission("SurveyList")]
        public async Task<IActionResult> List(FilterSurveyViewModel model)
        {
            var result = await _surveyService.FilterSurveyAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #region Create

        [Permission("CreateSurvey")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertSurveyViewModel());
        }

        [Permission("CreateSurvey")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertSurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _surveyService.CreateSurveyAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction("Update", new {id = result.Value});
        }

        #endregion

        #region Update

        [Permission("UpdateSurvey")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _surveyService.GetSurveyForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List));
            }

            return View(result.Value);
        }

        [Permission("UpdateSurvey")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertSurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _surveyService.UpdateSurveyAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return RedirectToAction("List");
        }

        #endregion

        #region Delete

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("DeleteSurvey")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            }

            var result = await _surveyService.DeleteSurveyAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            return new ResponseResult(true);
        }

        #endregion


        #endregion

        #region SurveyQuestion

        [Permission("SurveyQuestionsList")]
        public IActionResult SurveyQuestionsListViewComponent(int id)
        {
            return ViewComponent("SurveyQuestionsList", new { id = id });
        }

        [Permission("CreateSurveyQuestion")]
        public IActionResult CreateSurveyQuestion(int surveyId)
        {
            var model = new AdminSideUpsertSurveyQuestionViewModel();
            model.SurveyId = surveyId;

            return PartialView("_CreateSurveyQuestion", model);
        }

        [Permission("CreateSurveyQuestion")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> CreateSurveyQuestionAjax(AdminSideUpsertSurveyQuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values.SelectMany(v => v.Errors)
                    .First(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .ErrorMessage;

                return ResponseResult.Failure(message);
            }

            var result = await _surveyService.CreateSurveyQuestionAsync(model);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            return ResponseResult.Success(_localizer[result.Message].ToString());
        }

        [Permission("UpdateSurveyQuestion")]
        public async Task<IActionResult> UpdateSurveyQuestion(int id)
        {
            var result = await _surveyService.GetSurveyQuestionForEditByIdAsync(id);

            return PartialView("_UpdateSurveyQuestion", result.Value);
        }

        [Permission("UpdateSurveyQuestion")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> UpdateSurveyQuestionAjax(AdminSideUpsertSurveyQuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values.SelectMany(v => v.Errors)
                    .First(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .ErrorMessage;

                return ResponseResult.Failure(message);
            }

            var result = await _surveyService.UpdateSurveyQuestionAsync(model);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            return ResponseResult.Success(_localizer[result.Message].ToString());
        }

        [Permission("DeleteSurveyQuestion")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> DeleteSurveyQuestionAjax(int id)
        {
            var result = await _surveyService.DeleteSurveyQuestionAsync(id);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            return ResponseResult.Success(_localizer[result.Message].ToString());
        }

        #endregion

        #region SurveyAnswers

        [Permission("SurveyAnswersList")]
        public async Task<IActionResult> SurveyAnswersList(AdminSideFilterSurveyAnswerViewModel model)
        {
            var result = await _surveyService.FilterSurveyAnswerAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        [Permission("SurveyAnswerDetails")]
        public async Task<IActionResult> SurveyAnswerDetails(int surveyId, int answererId)
        {
            var result = await _surveyService.GetSurveyAnswerDetails(surveyId, answererId);

            if (result.IsFailure)
            {
                return NotFound();
            }

            return PartialView("_SurveyAnswerDetails", result.Value);
        }

        #endregion

        

        #endregion
    }
}
