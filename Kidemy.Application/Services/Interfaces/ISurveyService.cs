using Kidemy.Application.ViewModels.Survey.SurveyAnswersModels;
using Kidemy.Application.ViewModels.Survey.SurveyModels;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<Result> AnswerSurveyByUser(ClientSideSurveyAnswerViewModel model);
        Task<Result<int>> CreateSurveyAsync(AdminSideUpsertSurveyViewModel model);
        Task<Result> CreateSurveyQuestionAsync(AdminSideUpsertSurveyQuestionViewModel model);
        Task<Result<bool>> CurrentUserHasAnsweredTheSurvey(int surveyId);
        Task<Result> DeleteSurveyAsync(int id);
        Task<Result> DeleteSurveyQuestionAsync(int id);
        Task<Result<AdminSideFilterSurveyAnswerViewModel>> FilterSurveyAnswerAsync(AdminSideFilterSurveyAnswerViewModel model);
        Task<Result<FilterSurveyViewModel>> FilterSurveyAsync(FilterSurveyViewModel filter);
        Task<Result<FilterSurveyQuestionViewModel>> FilterSurveyQuestionAsync(FilterSurveyQuestionViewModel model);
        Task<Result<ClientSideSurveyViewModel>> GetActiveSurvey();
        Task<Result<int>> GetActiveSurveyIdAsync();
        Task<Result<AdminSideSurveyAnswerDetailsViewModel>> GetSurveyAnswerDetails(int surveyId, int answererId);
        Task<Result<AdminSideUpsertSurveyViewModel>> GetSurveyForEditByIdAsync(int id);
        Task<Result<AdminSideUpsertSurveyQuestionViewModel>> GetSurveyQuestionForEditByIdAsync(int id);
        Task<Result> UpdateSurveyAsync(AdminSideUpsertSurveyViewModel model);
        Task<Result> UpdateSurveyQuestionAsync(AdminSideUpsertSurveyQuestionViewModel model);
    }
}