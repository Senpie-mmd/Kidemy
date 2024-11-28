using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Survey;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class SurveyQuestionsListViewComponent : ViewComponent
    {

        #region Fields

        private readonly ISurveyService _surveyService;

        #endregion

        #region Ctor

        public SurveyQuestionsListViewComponent(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var survey = await _surveyService.FilterSurveyQuestionAsync(new FilterSurveyQuestionViewModel
            {
                SurveyId = id,
                TakeEntity = int.MaxValue
            });

            return View("SurveyQuestionsList", survey.Value);
        }

    }
}
