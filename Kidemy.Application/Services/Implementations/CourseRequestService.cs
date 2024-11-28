using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Domain.Events.Course.CourseQuestion;
using Kidemy.Domain.Events.CourseRequest;
using Kidemy.Domain.Interfaces.CourseRequest;
using Kidemy.Domain.Models.CourseRequest;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseRequestService : ICourseRequestService
    {
        #region Fields

        private readonly ICourseRequestRepository _courseRequestRepository;
        private readonly ICourseRequestVoteRepository _courseRequestVoteRepository;
        private readonly ICourseRequestMasterVolunteerRepository _courseRequestMasterVolunteerRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;

        #endregion

        #region Constructor
        public CourseRequestService(ICourseRequestRepository courseRequestRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler, ICourseRequestVoteRepository courseRequestVoteRepository, ICourseRequestMasterVolunteerRepository courseRequestMasterVolunteerRepository, IUserService userService)
        {
            _courseRequestRepository = courseRequestRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
            _courseRequestVoteRepository = courseRequestVoteRepository;
            _courseRequestMasterVolunteerRepository = courseRequestMasterVolunteerRepository;
            _userService = userService;
        }

        #endregion

        #region Methods

        public async Task<Result> CreateCourseRequestAsync(ClientSideCourseRequestRegisterViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            CourseRequest courseRequest = new()
            {
                RequestedById = model.RequestedById,
                Title = model.CourseRequestTitle,
                Description = model.Description,
                PreferedMasterId = model.PreferedMasterId,
                SelectedTags = string.Join(", ", model.SelectedTags ?? new List<string>()),
                CreatedDateOnUtc = DateTime.UtcNow
            };

            await _courseRequestRepository.InsertAsync(courseRequest);
            await _courseRequestRepository.SaveAsync();

            CourseRequestCreatedEvent courseRequestCreatedEvent = new(
                courseRequest.Id,
                courseRequest.RequestedById,
                courseRequest.Title,
                courseRequest.PreferedMasterId,
                courseRequest.SelectedTags,
                courseRequest.Description
                );

            await _mediatorHandler.PublishEvent(courseRequestCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<FilterForClientSideCourseRequestViewModel>> FilterForClientSideAsync(FilterForClientSideCourseRequestViewModel model)
        {
            if (model is null) return Result.Failure<FilterForClientSideCourseRequestViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<CourseRequest>();
            filterConditions.Add(c => c.IsConfirm == true);


            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _courseRequestRepository.FilterAsync(model, filterConditions, CourseRequestMapper.ClientMapCourseRequestViewModel);

            return model;
        }

        public async Task<Result> CreateCourseRequestVoteAsync(ClientSideCourseRequestVoteRegisterViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _courseRequestVoteRepository.AnyAsync(v => v.CourseRequestId == model.CourseRequestId && v.UserId == model.UserId))
            {
                return Result.Failure(ErrorMessages.YouAlreadyVotedThisCourse);
            }

            CourseRequestVote courseRequestVote = new()
            {
                CourseRequestId = model.CourseRequestId,
                UserId = model.UserId,
                IsAgree = true,
                CreatedDateOnUtc = DateTime.UtcNow
            };

            await _courseRequestVoteRepository.InsertAsync(courseRequestVote);
            await _courseRequestVoteRepository.SaveAsync();

            CourseRequestVoteCreatedEvent courseRequestVoteCreatedEvent = new(
            courseRequestVote.Id,
            courseRequestVote.CourseRequestId,
            courseRequestVote.UserId,
            courseRequestVote.IsAgree

      );

            await _mediatorHandler.PublishEvent(courseRequestVoteCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> CreateCourseRequestMasterVolunteerAsync(ClientSideCourseRequestMasterVolunteerRegisterViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _courseRequestMasterVolunteerRepository.AnyAsync(v => v.CourseRequestId == model.CourseRequestId && v.MasterId == model.MasterId))
            {
                return Result.Failure(ErrorMessages.YouAlreadyVotedThisCourse);
            }

            CourseRequestMasterVolunteer courseRequestMasterVolunteer = new()
            {
                CourseRequestId = model.CourseRequestId,
                MasterId = model.MasterId,
                MasterDescription = model.MasterDescription,
                CreatedDateOnUtc = DateTime.UtcNow
            };

            await _courseRequestMasterVolunteerRepository.InsertAsync(courseRequestMasterVolunteer);
            await _courseRequestMasterVolunteerRepository.SaveAsync();

            CourseRequestMasterVolunteerCreatedEvent courseRequestMasterVolunteerCreatedEvent = new(
            courseRequestMasterVolunteer.Id,
            courseRequestMasterVolunteer.CourseRequestId,
            courseRequestMasterVolunteer.MasterId,
            courseRequestMasterVolunteer.MasterDescription,
            courseRequestMasterVolunteer.AdminDescription

      );

            await _mediatorHandler.PublishEvent(courseRequestMasterVolunteerCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<FilterForAdminSideCourseRequestViewModel>> FilterForAdminSideAsync(FilterForAdminSideCourseRequestViewModel model)
        {
            if (model is null) return Result.Failure<FilterForAdminSideCourseRequestViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<CourseRequest>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(c => c.Title.Contains(model.Title));
            }


            if (model.IsConfirm != null)
            {
                filterConditions.Add(c => c.IsConfirm == model.IsConfirm);
            }

            await _courseRequestRepository.FilterAsync(model, filterConditions, CourseRequestMapper.AdminMapCourseRequestViewModel);

            return model;
        }

        public async Task<Result> ConfirmCourseRequest(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseRequest = await _courseRequestRepository.GetByIdAsync(id);
            if (courseRequest is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            courseRequest.IsConfirm = true;

            _courseRequestRepository.Update(courseRequest);
            await _courseRequestRepository.SaveAsync();

            CourseRequestConfirmedEvent courseRequestConfirmedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseRequestConfirmedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> NotConfirmCourseRequest(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var courseRequest = await _courseRequestRepository.GetByIdAsync(id);
            if (courseRequest is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            courseRequest.IsConfirm = false;

            _courseRequestRepository.Update(courseRequest);
            await _courseRequestRepository.SaveAsync();

            CourseRequestConfirmedEvent courseRequestConfirmedEvent = new(id);

            await _mediatorHandler.PublishEvent(courseRequestConfirmedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<AdminSideCoureRequestDetailsViewModel>> GetCourseRequestDetailsByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideCoureRequestDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var courseRequest = await _courseRequestRepository.GetByIdAsync(id, nameof(CourseRequest.MasterVolunteers), nameof(CourseRequest.Votes));

            if (courseRequest is null) return Result.Failure<AdminSideCoureRequestDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var userNames = await _userService.GetUsersProfileDetails(courseRequest.MasterVolunteers.Select(v => v.MasterId).ToList());

            string preferedMasterName = null;

            var userFullName = await _userService.GetUserFullName(courseRequest.PreferedMasterId ?? 0);

            if (userFullName.IsSuccess)
            {
                preferedMasterName = userFullName.Value;
            }

            var model = new AdminSideCoureRequestDetailsViewModel().MapFrom(courseRequest, userNames, preferedMasterName);

            return model;
        }

        public async Task<Result<int>> GetCountAsync(bool isConfirm)
            => await _courseRequestRepository.GetCountAsync(request => request.IsConfirm == isConfirm);

        #endregion
    }
}
