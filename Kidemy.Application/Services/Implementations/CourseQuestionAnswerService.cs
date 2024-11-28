using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestionAnswer;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer;
using Kidemy.Domain.Events.Course.CourseQuestionAnswer;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseQuestionAnswerService : ICourseQuestionAnswerService
    {
        #region Fields
        private readonly ICourseQuestionAnswerRepository _courseQuestionAnswerRepository;
        private readonly ICourseQuestionService _courseQuestionService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        #endregion

        #region Constructor
        public CourseQuestionAnswerService(ICourseQuestionAnswerRepository courseQuestionAnswerRepository, IMediatorHandler mediatorHandler, IUserService userService, ICourseQuestionService courseQuestionService, IRoleService roleService)
        {
            _courseQuestionAnswerRepository = courseQuestionAnswerRepository;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
            _courseQuestionService = courseQuestionService;
            _roleService = roleService;
        }


        #endregion

        #region Methods

        #region Filter

        #endregion

        #region Create
        public async Task<Result> CreateAsync(ClientSideUpsertCourseQuestionAnswerViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);


            CourseQuestionAnswer courseQuestionAnswer = new()
            {
                Answer = model.Answer,
                AnsweredById = model.AnsweredById,
                QuestionId = model.QuestionId,
                CreatedDateOnUtc = DateTime.UtcNow,

            };

            var isMaster = await _roleService.UserIsMasterNotAsync(model.AnsweredById);
            var hasAdminPermission =  _roleService.UserHasPermissionAsync(model.AnsweredById, "AdminPanel");
            if ( hasAdminPermission.Result.Value|| (isMaster.IsSuccess && isMaster.Value))
                courseQuestionAnswer.IsConfirmed = true;


            await _courseQuestionAnswerRepository.InsertAsync(courseQuestionAnswer);
            await _courseQuestionAnswerRepository.SaveAsync();

            CourseQuestionCreatedAnswerEvent courseQuestionAnswerCreatedEvent = new(
                model.QuestionId,
                model.AnsweredById,
                model.Answer
                );

            await _mediatorHandler.PublishEvent(courseQuestionAnswerCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region Get
        public async Task<Result<AdminSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByQuestionIdForAdminAsync(int questionId)
        {
            if (questionId == 0 && questionId != null) return Result.Failure<AdminSideFilterCourseQuestionAnswerViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseQuestionAnswer>();
            conditions.Add(n => n.QuestionId == questionId);

            AdminSideFilterCourseQuestionAnswerViewModel filter = new AdminSideFilterCourseQuestionAnswerViewModel();

            filter.TakeEntity = int.MaxValue;
            await _courseQuestionAnswerRepository.FilterAsync(filter, conditions, CourseQuestionAnswerMapper.MapAdminSideCourseQuestionAnswerViewModel, orderBy: ac => ac.CreatedDateOnUtc);

            var userIds = filter.Entities.Select(n => n.AnsweredById).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            filter.Entities = filter.Entities
                .Select(quesitons => quesitons
                .MapFromAdmin(usersInfo.Value.First(n => n.Id == quesitons.AnsweredById)))
                .ToList();

            var question = await _courseQuestionService.GetCourseQuestionsById(questionId);

            filter.QuestionTitle = question.Value.Title;
            filter.QuestionDescription = question.Value.Description;
            filter.UserName = question.Value.UserName;
            filter.UserProfile = question.Value.UserProfile;
            filter.CreateDate = question.Value.CreateDate;

            return filter;
        }
        public async Task<Result<ClientSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByQuestionIdForClientAsync(ClientSideFilterCourseQuestionAnswerViewModel model)
        {
            if (model.QuestionId == 0 && model.QuestionId != null) return Result.Failure<ClientSideFilterCourseQuestionAnswerViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseQuestionAnswer>();
            conditions.Add(n => n.QuestionId == model.QuestionId && n.IsConfirmed);

            await _courseQuestionAnswerRepository.FilterAsync(model, conditions, CourseQuestionAnswerMapper.MapClientSideCourseQuestionAnswerViewModel, orderBy: ac => ac.CreatedDateOnUtc);

            var userIds = model.Entities.Select(n => n.AnsweredById).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(quesitons => quesitons
                .MapFrom(usersInfo.Value.First(n => n.Id == quesitons.AnsweredById)))
                .ToList();

            var question = await _courseQuestionService.GetCourseQuestionsById(model.QuestionId);

            model.QuestionTitle = question.Value.Title;
            model.QuestionDescription = question.Value.Description;
            model.UserName = question.Value.UserName;
            model.UserProfile = question.Value.UserProfile;
            model.CreateDate = question.Value.CreateDate;
            model.AskedById = question.Value.AskedById;
            model.QuestionId = model.QuestionId;
            model.IsClosed = question.Value.IsClosed;
            model.CourseSlug = question.Value.CourseSlug;
            model.CourseTitle = question.Value.CourseTitle;

            return model;
        }

        public async Task<Result<ClientSideFilterCourseQuestionAnswerViewModel>> GetCourseQuestionAnswersByUserIdClientUserPanelAsync(ClientSideFilterCourseQuestionAnswerViewModel model)
        {
            if (model.UserId == 0 && model.UserId != null) return Result.Failure<ClientSideFilterCourseQuestionAnswerViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseQuestionAnswer>();
            conditions.Add(n => n.AnsweredById == model.UserId && n.Question.AskedById != model.UserId);

            await _courseQuestionAnswerRepository.FilterAsync(model, conditions, CourseQuestionAnswerMapper.MapClientSideCourseQuestionAnswerViewModel, orderByDesc: ac => ac.CreatedDateOnUtc);


            return model;
        }
        #endregion

        #region Delete

        public async Task<Result<int>> DeleteAsync(int id)
        {
            var question = await _courseQuestionAnswerRepository.GetByIdAsync(id);

            if (id <= 0) return Result.Failure<int>(ErrorMessages.ProcessFailedError);

            await _courseQuestionAnswerRepository.SoftDelete(id);
            await _courseQuestionAnswerRepository.SaveAsync();

            CourseQuestionAnswerDeletedEvent courseQuestionAnswerDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionAnswerDeletedEvent);



            return question.QuestionId;
        }

        #endregion

        #region Confirm

        public async Task<Result> ConfirmAsync(int id)
        {
            var question = await _courseQuestionAnswerRepository.GetByIdAsync(id);

            if (id <= 0) return Result.Failure<int>(ErrorMessages.ProcessFailedError);

            var answer = await _courseQuestionAnswerRepository.GetByIdAsync(id);

            if (answer is null) return Result.Failure<int>(ErrorMessages.ProcessFailedError);

            answer.IsConfirmed = true;

            _courseQuestionAnswerRepository.Update(answer);
            await _courseQuestionAnswerRepository.SaveAsync();

            CourseQuestionAnswerConfirmedEvent courseQuestionAnswerConfirmedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseQuestionAnswerConfirmedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }



        #endregion

        #endregion
    }
}
