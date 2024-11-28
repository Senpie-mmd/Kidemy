using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Survey.SurveyAnswersModels;
using Kidemy.Application.ViewModels.Survey.SurveyModels;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class SurveyController : BaseController
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

        [HttpGet("survey")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var surveyResult = await _surveyService.GetActiveSurvey();

            if (surveyResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[surveyResult.Message].ToString();
                return Redirect("/");
            }

            return View(surveyResult.Value);
        }

        [ActionName("Index")/*, ValidateReCaptcha*/]
        [HttpPost("survey"), ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ResponseResult> AnswerSurvey(ClientSideSurveyAnswerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault(m => !string.IsNullOrWhiteSpace(m));

                return ResponseResult.Failure(message);
            }

            var result = await _surveyService.AnswerSurveyByUser(model);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return ResponseResult.Success(_localizer[result.Message].ToString(), "/");
        }

        #endregion
    }
}
