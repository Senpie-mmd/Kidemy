using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Survey.SurveyAnswersModels;
using Kidemy.Application.ViewModels.Survey.SurveyModels;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;
using Kidemy.Domain.Enums.Survey;
using Kidemy.Domain.Events.Survey;
using Kidemy.Domain.Interfaces.Survey;
using Kidemy.Domain.Models.Survey;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class SurveyService : ISurveyService
    {
        #region Fields

        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyAnswerRepository _surveyAnswerRepository;
        private readonly ILogger<SurveyService> _logger;
        private readonly ILocalizingService _localizingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;
        private readonly IStringLocalizer _localizer;
        private readonly ISmsSenderSevice _smsSender;

        #endregion

        #region Constructor

        public SurveyService(ISurveyRepository surveyRepository,
            ISurveyAnswerRepository surveyAnswerRepository,
            ILogger<SurveyService> logger,
            ILocalizingService localizingService,
            IHttpContextAccessor httpContextAccessor,
            ISurveyQuestionRepository surveyQuestionRepository,
            IMediatorHandler mediatorHandler,
            IUserService userService,
            IStringLocalizer localizer,
            ISmsSenderSevice smsSender)
        {
            _surveyRepository = surveyRepository;
            _surveyAnswerRepository = surveyAnswerRepository;
            _logger = logger;
            _localizingService = localizingService;
            _httpContextAccessor = httpContextAccessor;
            _surveyQuestionRepository = surveyQuestionRepository;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
            _localizer = localizer;
            _smsSender = smsSender;
        }

        #endregion

        #region Methods

        #region Survey

        public async Task<Result<FilterSurveyViewModel>> FilterSurveyAsync(FilterSurveyViewModel filter)
        {
            if (filter == null) return Result.Failure<FilterSurveyViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<Survey>();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(s => s.Title.Contains(filter.Title));
            }

            if (filter.IsPublished is not null)
            {
                conditions.Add(s => s.IsPublished == filter.IsPublished);
            }

            await _surveyRepository.FilterAsync(filter, conditions, SurveyMapper.MapAdminSideSurveyViewModel);

            await _localizingService.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result<AdminSideUpsertSurveyViewModel>> GetSurveyForEditByIdAsync(int id)
        {
            if (id < 0) return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);

            var survey = await _surveyRepository.GetByIdAsync(id);

            if (survey is null)
            {
                _logger.LogError("SurveyService: GetSurveyForEditByIdAsync: Not found survey by id: " + id);
                return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);
            }

            var model = new AdminSideUpsertSurveyViewModel().MapFrom(survey);

            await _localizingService.FillModelToEditByAdminAsync(survey, model);

            return model;
        }

        public async Task<Result<int>> CreateSurveyAsync(AdminSideUpsertSurveyViewModel model)
        {
            if (model is null) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            if (await _surveyRepository.AnyAsync(s => s.Title == model.Title))
            {
                return Result.Failure<int>(ErrorMessages.DuplicatedTitleError);
            }

            if (model.IsPublished)
            {
                await _surveyRepository.UnpublishAllPublishedSurveys();
                await _mediatorHandler.PublishEvent(new UnpublishedAllPublishedSurveysEvent());
            }

            var survey = new Survey
            {
                Title = model.Title,
                IsPublished = model.IsPublished
            };

            await _surveyRepository.InsertAsync(survey);
            await _surveyRepository.SaveAsync();

            await _localizingService.SaveLocalizations(survey, model);

            var createdEvenet = new SurveyCreatedEvent(survey.Title, survey.IsPublished);
            await _mediatorHandler.PublishEvent(createdEvenet);

            return survey.Id;
        }

        public async Task<Result> UpdateSurveyAsync(AdminSideUpsertSurveyViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var survey = await _surveyRepository.GetByIdAsync(model.Id);

            if (survey is null)
            {
                _logger.LogError("SurveyService: UpdateSurveyAsync: Not found survey by id: " + model.Id);
                return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);
            }

            var surveyCopy = survey.DeepCopy<Survey>();

            if (model.IsPublished && !survey.IsPublished)
            {
                await _surveyRepository.UnpublishAllPublishedSurveys();
                await _mediatorHandler.PublishEvent(new UnpublishedAllPublishedSurveysEvent());
            }

            survey.Title = model.Title;
            survey.IsPublished = model.IsPublished;
            survey.UpdatedDateOnUtc = DateTime.UtcNow;

            _surveyRepository.Update(survey);
            await _surveyRepository.SaveAsync();

            await _localizingService.SaveLocalizations(survey, model);

            var SurveyUpdatedEvent = new SurveyUpdatedEvent(
                model.Id,
                surveyCopy.Title,
                surveyCopy.IsPublished,
                model.Title,
                model.IsPublished);

            await _mediatorHandler.PublishEvent(SurveyUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> DeleteSurveyAsync(int id)
        {
            if (id < 0) return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);

            var survey = await _surveyRepository.GetByIdAsync(id);

            if (survey is null)
            {
                _logger.LogError("SurveyService: DeleteSurveyAsync: Not found survey by id: " + id);
                return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);
            }

            survey.IsDeleted = true;
            survey.UpdatedDateOnUtc = DateTime.UtcNow;

            _surveyRepository.Update(survey);
            await _surveyRepository.SaveAsync();

            var SurveyDeletedEvent = new SurveyDeletedEvent(id);
            await _mediatorHandler.PublishEvent(SurveyDeletedEvent);

            return Result.Success();
        }

        public async Task<Result<ClientSideSurveyViewModel>> GetActiveSurvey()
        {
            var survey = await _surveyRepository.FirstOrDefaultAsync(s => s.IsPublished, includeProperties: nameof(Survey.Questions));

            if (survey is null)
            {
                _logger.LogError("SurveyService: GetActiveSurvey: Not found active survey");
                return Result.Failure<ClientSideSurveyViewModel>(ErrorMessages.NotFoundActiveSurveyError);
            }

            var model = new ClientSideSurveyViewModel().MapFrom(survey);

            await _localizingService.TranslateModelAsync(model);
            await _localizingService.TranslateModelAsync(model.Questions);

            return model;
        }

        public async Task<Result<int>> GetActiveSurveyIdAsync()
        {
            var survey = await _surveyRepository.FirstOrDefaultAsync(s => s.IsPublished);

            if (survey is null)
            {
                _logger.LogError("SurveyService: GetActiveSurveyIdAsync: Not found active survey");
                return Result.Failure<int>(ErrorMessages.NotFoundActiveSurveyError);
            }

            return survey.Id;
        }

        public async Task<Result> AnswerSurveyByUser(ClientSideSurveyAnswerViewModel model)
        {
            if (model is null)
            {
                _logger.LogError("SurveyService: AnswerSurveyByUser: model is null");
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            // check null answers
            if (model.QuestionAnswers.Any(a => string.IsNullOrWhiteSpace(string.Join(",", a.Answer))))
            {
                return Result.Failure(ErrorMessages.PleaseFillAllRequiredFieldsError);
            }

            var survey = await _surveyRepository.GetByIdAsync(model.SurveyId, includeProperties: nameof(Survey.Questions));

            if (survey is null)
            {
                _logger.LogError("SurveyService: AnswerSurveyByUser: not found survey by id : " + model.SurveyId);
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            // check null answers by using database data
            var allQuestionsAreAnswered = survey.Questions.All(q => model.QuestionAnswers.Select(a => a.QuestionId).Contains(q.Id));
            if (!allQuestionsAreAnswered)
            {
                return Result.Failure(ErrorMessages.PleaseFillAllRequiredFieldsError);
            }

            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            var userHasAnsweredTheSurvey = await _surveyAnswerRepository
                .AnyAsync(a => a.AnswererId == userId && survey.Questions.Select(q => q.Id).Contains(a.QuestionId));

            if (userHasAnsweredTheSurvey)
            {
                return Result.Failure(ErrorMessages.YouAlreadyHaveAnsweredTheSurveyError);
            }

            var answers = model.QuestionAnswers.Select(a => new SurveyAnswer
            {
                AnswererId = userId,
                AnswerText = string.Join(",", a.Answer),
                QuestionId = a.QuestionId
            }).ToList();

            await _surveyAnswerRepository.InsertRangeAsync(answers);
            await _surveyAnswerRepository.SaveAsync();

            var surveyAnsweredEvent = new SurveyAnsweredEvent(
                survey.Id,
                userId,
                answers.Select(a => (a.QuestionId, a.AnswerText)).ToList());

            await _mediatorHandler.PublishEvent(surveyAnsweredEvent);

            return Result.Success();
        }

        public async Task<Result<bool>> CurrentUserHasAnsweredTheSurvey(int surveyId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            return await _surveyAnswerRepository
                .AnyAsync(answer => answer.AnswererId == currentUserId && answer.Question.SurveyId == surveyId);
        }

        #endregion

        #region SurveyQuestion

        public async Task<Result<FilterSurveyQuestionViewModel>> FilterSurveyQuestionAsync(FilterSurveyQuestionViewModel model)
        {
            if (model is null) return Result.Failure<FilterSurveyQuestionViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<SurveyQuestion>();

            if (model.QuestionType is not null)
            {
                conditions.Add(q => q.Type == model.QuestionType);
            }

            if (model.SurveyId is not null)
            {
                conditions.Add(q => q.SurveyId == model.SurveyId);
            }

            await _surveyQuestionRepository.FilterAsync(model, conditions, SurveyMapper.MapAdminSideSurveyQuestionViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;
        }

        public async Task<Result<AdminSideUpsertSurveyQuestionViewModel>> GetSurveyQuestionForEditByIdAsync(int id)
        {
            if (id < 0) return Result.Failure<AdminSideUpsertSurveyQuestionViewModel>(ErrorMessages.FailedOperationError);

            var surveyQuestion = await _surveyQuestionRepository.GetByIdAsync(id);

            if (surveyQuestion is null)
            {
                _logger.LogError("SurveyService: GetSurveyQuestionForEditByIdAsync: Not found survey question by id: " + id);
                return Result.Failure<AdminSideUpsertSurveyQuestionViewModel>(ErrorMessages.FailedOperationError);
            }

            var model = new AdminSideUpsertSurveyQuestionViewModel().MapFrom(surveyQuestion);

            await _localizingService.FillModelToEditByAdminAsync(surveyQuestion, model);

            return model;
        }

        public async Task<Result> CreateSurveyQuestionAsync(AdminSideUpsertSurveyQuestionViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.Type != SurveyQuestionType.Descriptive && string.IsNullOrWhiteSpace(model.Options))
            {
                return Result.Failure(ErrorMessages.QuestionOptionsAreRequiredError);
            }

            if (await _surveyQuestionRepository.AnyAsync(s => s.Title == model.Title && s.SurveyId == model.SurveyId))
            {
                return Result.Failure(ErrorMessages.DuplicatedTitleError);
            }

            var surveyQuestion = new SurveyQuestion
            {
                Title = model.Title,
                SurveyId = model.SurveyId,
                Type = model.Type,
                Priority = model.Priority,
                Options = model.Type != SurveyQuestionType.Descriptive ? model.Options : null
            };

            if (model.Type == SurveyQuestionType.Descriptive)
            {
                model.LocalizedModels.ForEach(l => l.Options = null);
            }

            await _surveyQuestionRepository.InsertAsync(surveyQuestion);
            await _surveyQuestionRepository.SaveAsync();

            await _localizingService.SaveLocalizations(surveyQuestion, model);

            var SurveyQuestionCreatedEvent = new SurveyQuestionCreatedEvent(
                surveyQuestion.Title,
                surveyQuestion.Type,
                surveyQuestion.SurveyId,
                surveyQuestion.Priority,
                surveyQuestion.Options);

            await _mediatorHandler.PublishEvent(SurveyQuestionCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateSurveyQuestionAsync(AdminSideUpsertSurveyQuestionViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.Type != SurveyQuestionType.Descriptive && string.IsNullOrWhiteSpace(model.Options))
            {
                return Result.Failure(ErrorMessages.QuestionOptionsAreRequiredError);
            }

            var surveyQuestion = await _surveyQuestionRepository.GetByIdAsync(model.Id);
            if (surveyQuestion is null)
            {
                _logger.LogError("SurveyService: UpdateSurveyQuestionAsync: Not found survey by id: " + model.Id);
                return Result.Failure<AdminSideUpsertSurveyViewModel>(ErrorMessages.FailedOperationError);
            }

            var questionCopy = surveyQuestion.DeepCopy<SurveyQuestion>();

            surveyQuestion.Title = model.Title;
            surveyQuestion.SurveyId = model.SurveyId;
            surveyQuestion.Type = model.Type;
            surveyQuestion.Priority = model.Priority;
            surveyQuestion.Options = model.Type != SurveyQuestionType.Descriptive ? model.Options : null;
            surveyQuestion.UpdatedDateOnUtc = DateTime.UtcNow;

            if (model.Type == SurveyQuestionType.Descriptive)
            {
                model.LocalizedModels.ForEach(l => l.Options = null);
            }

            _surveyQuestionRepository.Update(surveyQuestion);
            await _surveyQuestionRepository.SaveAsync();

            await _localizingService.SaveLocalizations(surveyQuestion, model);

            var SurveyQuestionUpdatedEvent = new SurveyQuestionUpdatedEvent(
                questionCopy.Id,
                questionCopy.Title,
                questionCopy.SurveyId,
                questionCopy.Type,
                questionCopy.Priority,
                questionCopy.Options,
                surveyQuestion.Title,
                surveyQuestion.SurveyId,
                surveyQuestion.Type,
                surveyQuestion.Priority,
                surveyQuestion.Options);

            await _mediatorHandler.PublishEvent(SurveyQuestionUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> DeleteSurveyQuestionAsync(int id)
        {
            if (id < 0) return Result.Failure(ErrorMessages.FailedOperationError);

            var survey = await _surveyQuestionRepository.GetByIdAsync(id);

            if (survey is null)
            {
                _logger.LogError("SurveyService: DeleteSurveyQuestionAsync: Not found survey by id: " + id);
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            survey.IsDeleted = true;
            survey.UpdatedDateOnUtc = DateTime.UtcNow;

            _surveyQuestionRepository.Update(survey);
            await _surveyQuestionRepository.SaveAsync();

            var SurveyQuestionDeletedEvent = new SurveyQuestionDeletedEvent(id);
            await _mediatorHandler.PublishEvent(SurveyQuestionDeletedEvent);

            return Result.Success();
        }

        #endregion

        #region SurveyAnswers

        public async Task<Result<AdminSideFilterSurveyAnswerViewModel>> FilterSurveyAnswerAsync(AdminSideFilterSurveyAnswerViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterSurveyAnswerViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<SurveyAnswer>();

            if (model.SurveyId is not null)
            {
                conditions.Add(s => s.Question.SurveyId == model.SurveyId);
            }

            if (model.UserId is not null)
            {
                conditions.Add(s => s.AnswererId == model.UserId);
            }

            if (model.FromDate is not null)
            {
                conditions.Add(s => s.CreatedDateOnUtc >= model.FromDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            if (model.ToDate is not null)
            {
                conditions.Add(s => s.CreatedDateOnUtc <= model.ToDate.ConvertToEnglishNumber().ParseUserDateToUTC());
            }

            await _surveyAnswerRepository.FilterAsync(model, conditions, SurveyMapper.MapAdminSideSurveyAnswerViewModel);

            var usersFullName = await _userService.GetUsersFullNames(model.Entities.Select(s => s.AnswererId).ToList());

            model.Entities.ForEach(answer =>
            {
                answer.AnswererFullName = usersFullName.FirstOrDefault(u => u.UserId == answer.AnswererId)?.UserFullName ?? "-";
            });

            return model;
        }

        public async Task<Result<AdminSideSurveyAnswerDetailsViewModel>> GetSurveyAnswerDetails(int surveyId, int answererId)
        {
            if (surveyId <= 0 || answererId <= 0)
            {
                return Result.Failure<AdminSideSurveyAnswerDetailsViewModel>(ErrorMessages.FailedOperationError);
            }

            var surveyAnswers = await _surveyAnswerRepository.GetUserAnswersForSurveyIdAsync(surveyId, answererId);

            if (surveyAnswers is null || !surveyAnswers.Any())
            {
                return Result.Failure<AdminSideSurveyAnswerDetailsViewModel>(ErrorMessages.FailedOperationError);
            }


            var model = new AdminSideSurveyAnswerDetailsViewModel().MapFrom(surveyAnswers);

            var result = await _userService.GetUserFullName(surveyAnswers.FirstOrDefault()?.AnswererId ?? 0);

            if (result.IsSuccess)
            {
                model.AnswererFullName = result.Value;
            }
            await _localizingService.TranslateModelAsync(model);
            await _localizingService.TranslateModelAsync(model.Answers);

            return model;
        }

        #endregion

        #endregion
    }
}
