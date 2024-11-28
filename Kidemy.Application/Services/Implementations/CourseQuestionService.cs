using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Events.Course.CourseQuestion;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseQuestionService : ICourseQuestionService
    {
        #region Methods
        private ICourseQuestionRepository _courseQuestionRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public CourseQuestionService(ICourseQuestionRepository courseQuestionRepository, IMediatorHandler mediatorHandler, ICourseService courseService, IUserService userService)
        {
            _courseQuestionRepository = courseQuestionRepository;
            _mediatorHandler = mediatorHandler;
            _courseService = courseService;
            _userService = userService;
        }

        #endregion

        #region Methods

        #region Create
        public async Task<Result> CreateAsync(ClientSideUpsertCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.CourseSlug is not null)
                model.CourseId = await _courseService.GetCourseIdBySlug(model.CourseSlug);
            else
                return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseTypeResult = await _courseService.GetCourseType(model.CourseId);
            if (courseTypeResult.IsFailure) return Result.Failure(ErrorMessages.FailedOperationError);

            if (courseTypeResult.Value != CourseType.Free)
            {
                var userHasCourse = await _courseService.UserHasCourseAsync(model.CourseId, model.UserId);
                if (!userHasCourse.Value)
                    return Result.Failure(ErrorMessages.OnlyBuyerCanAskQuestion);
            }

            CourseQuestion courseQuestion = new()
            {
                Title = model.Title,
                Description = model.Description,
                CourseId = model.CourseId,
                AskedById = model.UserId,
                CreatedDateOnUtc = DateTime.UtcNow,
                IsConfirmed = false,
            };

            await _courseQuestionRepository.InsertAsync(courseQuestion);
            await _courseQuestionRepository.SaveAsync();

            CourseQuestionCreatedEvent courseQuestionCreatedEvent = new(
                model.Title,
                model.Description,
                model.UserId,
                model.CourseId,
                model.IsClosed);

            await _mediatorHandler.PublishEvent(courseQuestionCreatedEvent);

            return Result.Success(SuccessMessages.YourQuestionIsAwaitingApproval);
        }

        #endregion

        #region Update
        public async Task<Result> UpdateAsync(ClientSideUpsertCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseQuestion = await _courseQuestionRepository.GetByIdAsync(model.Id);

            if (courseQuestion is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedcourseQuestion = courseQuestion?.DeepCopy<CourseQuestion>();

            courseQuestion.Title = model.Title;
            courseQuestion.Description = model.Description;
            courseQuestion.IsClosed = model.IsClosed;

            _courseQuestionRepository.Update(courseQuestion);
            await _courseQuestionRepository.SaveAsync();

            CourseQuestionUpdatedEvent courseQuestionCreatedEvent = new(
               copiedcourseQuestion.Id,
               copiedcourseQuestion.Title,
              model.Title,
              copiedcourseQuestion.Description,
              model.Description,
              model.UserId,
              model.CourseId,
              copiedcourseQuestion.IsClosed,
              model.IsClosed);
            await _mediatorHandler.PublishEvent(courseQuestionCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }
        #endregion

        #region Filter
        public async Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterAsync(AdminSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterCourseQuestionViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<CourseQuestion>();

            if (model.CourseId != 0 && model.CourseId != null)
                filterConditions.Add(x => x.CourseId == model.CourseId);

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            switch (model.QuestionState)
            {
                case CourseQuestionState.All:
                    break;
                case CourseQuestionState.Confirmed:
                    filterConditions.Add(x => x.IsConfirmed);
                    break;
                case CourseQuestionState.NotConfirmed:
                    filterConditions.Add(x => !x.IsConfirmed);
                    break;
            }

            await _courseQuestionRepository.FilterAsync(model, filterConditions, CourseQuestionMapper.MapAdminSideCourseQuestionViewModel, orderByDesc: ac => ac.UpdatedDateOnUtc);

            return model;
        }

        public async Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterNotConfirmedAsync(AdminSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterCourseQuestionViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<CourseQuestion>();

            filterConditions.Add(x => !x.IsClosed && (!x.IsConfirmed || x.Answers.Any(answer => !answer.IsConfirmed)));

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _courseQuestionRepository.FilterAsync(model, filterConditions, CourseQuestionMapper.MapAdminSideCourseQuestionViewModel, orderByDesc: ac => ac.UpdatedDateOnUtc);

            return model;
        }

        public async Task<Result<AdminSideFilterCourseQuestionViewModel>> FilterAdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursAsync(AdminSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterCourseQuestionViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<CourseQuestion>();


            filterConditions.Add(x => x.IsConfirmed && !x.IsClosed &&
            (
            ((x.CreatedDateOnUtc.AddDays(14).Date <= DateTime.UtcNow.Date) && (!x.Answers.Any(z => z.AnsweredById == x.Course.MasterId)))
            ||
            ((x.UserReaction != UserReaction.Useful) && (x.Answers.OrderBy(s => s.Id).LastOrDefault().AnsweredById == x.AskedById))
            ));

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }



            await _courseQuestionRepository.FilterAsync(model, filterConditions, CourseQuestionMapper.MapAdminSideCourseQuestionViewModel, orderByDesc: ac => ac.UpdatedDateOnUtc);
            var userIds = model.Entities.Select(n => n.MasterId).ToList();
            var usersInfo = await _userService.GetUserNameAndMobileForMasterByUserId(userIds);
            model.Entities = model.Entities
                .Select(quesitons => quesitons
                .MapFrom(usersInfo.Value.First(n => n.Id == quesitons.MasterId)))
                .ToList();

            return model;
        }
        #endregion

        #region Get

        public async Task<Result<ClientSideCourseQuestionViewModel>> GetCourseQuestionsById(int id)
        {
            if (id == null || id == 0)
                return Result.Failure<ClientSideCourseQuestionViewModel>(ErrorMessages.FailedOperationError);

            var model = await _courseQuestionRepository.GetByIdAsync(id);
            var userIds = new List<int>();

            userIds.Add(model.AskedById);
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            var course = await _courseService.GetCourseById(model.CourseId);
            var viewModel = new ClientSideCourseQuestionViewModel().MapFrom(model);
            viewModel.UserName = usersInfo.Value.FirstOrDefault().UserName;
            viewModel.UserProfile = usersInfo.Value.FirstOrDefault().UserAvatar;
            viewModel.CourseSlug = course.Value.Slug;
            viewModel.CourseTitle = course.Value.Title;
            return viewModel;

        }

        public async Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterCourseQuestionsBySlug(ClientSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterCourseQuestionViewModel>(ErrorMessages.FailedOperationError);
            var conditions = Filter.GenerateConditions<CourseQuestion>();
            if (!string.IsNullOrEmpty(model.CourseSlug))
            {

                int courseId = await _courseService.GetCourseIdBySlug(model.CourseSlug);
                if (courseId < 1) return Result.Failure<ClientSideFilterCourseQuestionViewModel>(ErrorMessages.FailedOperationError);
                conditions.Add(n => n.CourseId == courseId && n.IsConfirmed);
                var course = await _courseService.GetCourseById(courseId);
                model.CourseTitle = course.Value.Title;
            }

            await _courseQuestionRepository.FilterAsync(model, conditions, CourseQuestionMapper.MapClientSideCourseQuestionViewModel, orderByDesc: x => x.CreatedDateOnUtc);

            var userIds = model.Entities.Select(n => n.AskedById).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(quesitons => quesitons
                .MapFrom(usersInfo.Value.First(n => n.Id == quesitons.AskedById)))
                .ToList();

            return model;
        }


        public async Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterCourseQuestionsByUserId(ClientSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterCourseQuestionViewModel>(ErrorMessages.FailedOperationError);


            var conditions = Filter.GenerateConditions<CourseQuestion>();
            conditions.Add(n => n.AskedById == model.AskedById && n.IsConfirmed);



            await _courseQuestionRepository.FilterAsync(model, conditions, CourseQuestionMapper.MapClientSideCourseQuestionViewModel, orderByDesc: x => x.CreatedDateOnUtc);


            return model;
        }

        public async Task<Result<ClientSideFilterCourseQuestionViewModel>> FilterNotAnsweredCourseQuestionsByMasterId(ClientSideFilterCourseQuestionViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterCourseQuestionViewModel>(ErrorMessages.FailedOperationError);


            var conditions = Filter.GenerateConditions<CourseQuestion>();

            conditions.Add(n =>
            n.Course.MasterId == model.TeacherId
            && n.IsConfirmed
            && (!n.Answers.Any(x => x.AnsweredById == model.TeacherId)
            || (n.UserReaction == UserReaction.Useless
            && n.Answers.OrderBy(s => s.Id).LastOrDefault().AnsweredById == n.AskedById)));





            await _courseQuestionRepository.FilterAsync(model, conditions, CourseQuestionMapper.MapClientSideCourseQuestionViewModel, orderByDesc: x => x.CreatedDateOnUtc);


            return model;
        }

        #endregion

        #region Delete
        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _courseQuestionRepository.SoftDelete(id);
            await _courseQuestionRepository.SaveAsync();

            CourseQuestionDeletedEvent courseQuestionDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region OpenAndCloseCourseQuestion

        public async Task<Result> CloseCourseQuestionAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseQuestion = await _courseQuestionRepository.GetByIdAsync(id);
            if (courseQuestion is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            courseQuestion.IsClosed = true;

            _courseQuestionRepository.Update(courseQuestion);
            await _courseQuestionRepository.SaveAsync();

            CourseQuestionClosedEvent courseQuestionClosedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionClosedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> OpenCourseQuestionAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseQuestion = await _courseQuestionRepository.GetByIdAsync(id);
            if (courseQuestion is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            courseQuestion.IsClosed = false;

            _courseQuestionRepository.Update(courseQuestion);
            await _courseQuestionRepository.SaveAsync();

            CourseQuestionClosedEvent courseQuestionClosedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionClosedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #region ConfrimCourseQuestion

        public async Task<Result> ConfrimCourseQuestion(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseQuestion = await _courseQuestionRepository.GetByIdAsync(id);
            if (courseQuestion is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            courseQuestion.IsConfirmed = true;

            _courseQuestionRepository.Update(courseQuestion);
            await _courseQuestionRepository.SaveAsync();

            CourseRequestConfirmedEvent courseQuestionConfirmedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionConfirmedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region User FeedBack
        public async Task<Result> ChangeUserFeedBack(int id, bool feedBack)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);


            var question = await _courseQuestionRepository.GetByIdAsync(id);


            var lastUserReaction = question.UserReaction;


            if (feedBack)
            {
                question.UserReaction = UserReaction.Useful;
                question.IsClosed = true;
            }

            else
                question.UserReaction = UserReaction.Useless;

            await _courseQuestionRepository.SaveAsync();

            CourseQuestionUserFeedbackEvent courseQuestionUserFeedbackEvent = new(lastUserReaction, question.UserReaction, question.IsClosed);

            await _mediatorHandler.PublishEvent(courseQuestionUserFeedbackEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #region Close Question 

        public async Task<Result> CloseCourseQuestionAfter1MonthAsync()
        {
            // Closing every question that 30 days have passed since its last owner's answer to his question 

            var questions = await _courseQuestionRepository.GetCourseQuestionAfter1MonthAsync();

            if (questions != null && questions.Any())
            {
                foreach (var question in questions)
                {
                    question.IsClosed = true;
                    _courseQuestionRepository.Update(question);
                }
                await _courseQuestionRepository.SaveAsync();
                return Result.Success();
            }
            return Result.Failure(ErrorMessages.NullValue);
        }

        #endregion

        #endregion

        #region Utilities

        #endregion
    }
}
