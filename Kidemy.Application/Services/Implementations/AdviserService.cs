using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Domain.Events.Consultation.Adviser;
using Kidemy.Domain.Events.Consultation.AdviserAvailableDate;
using Kidemy.Domain.Events.Consultation.AdviserConsultationType;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class AdviserService : IAdviserSerivce
    {
        #region Fields
        private readonly IAdviserAvailableDateRepository _adviserAvailableDateRepository;
        private readonly IAdviserConsultationTypeRespositry _adviserConsultationTypeRespositry;
        private readonly IAdviserRepository _adviserRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;
        private readonly IMasterService  _masterService;
        #endregion

        #region Constructor
        public AdviserService(IAdviserAvailableDateRepository adviserAvailableDateRepository, IAdviserRepository adviserRepository, IAdviserConsultationTypeRespositry adviserConsultationTypeRespositry, IMediatorHandler mediatorHandler, IUserService userService, IMasterService masterService)
        {
            _adviserAvailableDateRepository = adviserAvailableDateRepository;
            _adviserRepository = adviserRepository;
            _adviserConsultationTypeRespositry = adviserConsultationTypeRespositry;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
            _masterService = masterService;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Result> CreateAdviserAsync(AdminSideUpsertAdviserViewModel model)
        {

            #region Validations 

            if (model is null) return Result.Failure(ErrorMessages.NullValue);
            if (model.UserId == null || model.UserId <= 0) return Result.Failure(ErrorMessages.UserIsRequiredError);

            if (model.AdviserAvailableDates is null || !model.AdviserAvailableDates.Any())
                return Result.Failure(ErrorMessages.InsertAdviserAvailableDates);

            if (model.AdviserAvailableDates.Any(x => x.StartTime >= x.EndTime))
                return Result.Failure(ErrorMessages.EndTimeMustBeLaterThanStartTimeError);

            if (model.AdviserConsultationTypes is null || !model.AdviserConsultationTypes.Any())
                return Result.Failure(ErrorMessages.InsertAdviserConsultationTypes);

            var isduplicateAdviser = await _adviserRepository.FirstOrDefaultAsync(x => x.UserId == model.UserId && !x.IsDeleted);
            if (isduplicateAdviser != null) return Result.Failure(ErrorMessages.DuplicatedAdviserError);

            var isduplicatePriority = await _adviserRepository.FirstOrDefaultAsync(x => x.Priority == model.Priority && !x.IsDeleted);
            if (isduplicatePriority != null) return Result.Failure(ErrorMessages.DuplicatedPriorityError);

            #endregion

            var adviser = new Adviser()
            {
                IsPublished = model.IsPublished,
                ConsultationPercentage = model.ConsultationPercentage.Value,
                Priority = model.Priority.Value,
                UserId = model.UserId,
            };

            await _adviserRepository.InsertAsync(adviser);
            await _adviserRepository.SaveAsync();

            #region Create AdviserAvailableDates


            foreach (var availableDate in model.AdviserAvailableDates)
            {
                var adviserAvailableDate = new AdviserAvailableDate()
                {
                    AdviserId = adviser.Id,
                    DayOfWeek = availableDate.DayOfWeek,
                    StartTime = availableDate.StartTime.Value,
                    EndTime = availableDate.EndTime.Value,
                };
                await _adviserAvailableDateRepository.InsertAsync(adviserAvailableDate);

            }

            #endregion

            #region Create AdviserConsultationTypes

            foreach (var consultationType in model.AdviserConsultationTypes)
            {
                var adviserConsultationType = new AdviserConsultationType()
                {
                    AdviserId = adviser.Id,
                    Title = consultationType.Title,
                    Price = consultationType.Price.GetValueOrDefault(),
                    IsPublished = true,
                };
                await _adviserConsultationTypeRespositry.InsertAsync(adviserConsultationType);
            }
            #endregion

            await _adviserRepository.SaveAsync();

            #region AdviserCreatedEvent

            var adviserCreatedEvent = new AdviserCreatedEvent(
             adviser.Id,
             adviser.IsPublished,
             adviser.ConsultationPercentage,
             adviser.Priority,
             adviser.UserId
             );

            await _mediatorHandler.PublishEvent(adviserCreatedEvent);

            #endregion

            #region AdviserAvailableDateCreatedEvent
            foreach (var availableDate in model.AdviserAvailableDates)
            {
                var adviserAvailableDateCreatedEvent = new AdviserAvailableDateCreatedEvent(
               availableDate.Id,
           adviser.Id,
 availableDate.DayOfWeek,
 availableDate.StartTime.Value,
 availableDate.EndTime.Value
             );
                await _mediatorHandler.PublishEvent(adviserAvailableDateCreatedEvent);

            }

            #endregion

            #region AdviserConsultationType

            foreach (var consultationType in model.AdviserConsultationTypes)
            {
                var adviserConsultationTypeCreatedEvent = new AdviserConsultationTypeCreatedEvent(
                    consultationType.Id,
                    adviser.Id,
                    consultationType.Title,
                    consultationType.Price.Value,
                    consultationType.IsPublished
                    );
                await _mediatorHandler.PublishEvent(adviserConsultationTypeCreatedEvent);

            }

            #endregion

            return Result.Success();

        }

        #endregion

        #region Update
        public async Task<Result> EditAdviserAsync(AdminSideUpsertAdviserViewModel model)
        {
            #region Validations 

            if (model is null) return Result.Failure(ErrorMessages.NullValue);
            if (model.UserId == null || model.UserId <= 0) return Result.Failure(ErrorMessages.UserIsRequiredError);

            if (model.AdviserAvailableDates is null || !model.AdviserAvailableDates.Any())
                return Result.Failure(ErrorMessages.InsertAdviserAvailableDates);

            if (model.AdviserAvailableDates.Any(x => x.StartTime == null || x.EndTime == null || x.StartTime >= x.EndTime))
                return Result.Failure(ErrorMessages.EndTimeMustBeLaterThanStartTimeError);

            if (model.AdviserConsultationTypes is null || !model.AdviserConsultationTypes.Any())
                return Result.Failure(ErrorMessages.InsertAdviserConsultationTypes);

            var isduplicateAdviser = await _adviserRepository.FirstOrDefaultAsync(x => x.UserId == model.UserId && !x.IsDeleted && x.Id != model.Id);
            if (isduplicateAdviser != null) return Result.Failure(ErrorMessages.DuplicatedAdviserError);

            var isduplicatePriority = await _adviserRepository.FirstOrDefaultAsync(x => x.Priority == model.Priority && !x.IsDeleted && x.Id != model.Id);
            if (isduplicatePriority != null) return Result.Failure(ErrorMessages.DuplicatedPriorityError);



            #endregion

            var adviser = await _adviserRepository.GetByIdAsync(model.Id);

            adviser.ConsultationPercentage = model.ConsultationPercentage.Value;
            adviser.Priority = model.Priority.Value;

            adviser.IsPublished = model.IsPublished;

            _adviserRepository.Update(adviser);

            #region SelectedDate


            var deletedAdviserAvailableDates = await _adviserAvailableDateRepository.GetAllAsync(x => !x.IsDeleted && x.AdviserId == adviser.Id);

            if (deletedAdviserAvailableDates != null && deletedAdviserAvailableDates.Any())
            {
                foreach (var date in deletedAdviserAvailableDates)
                {
                    date.IsDeleted = true;
                }
                _adviserAvailableDateRepository.UpdateRange(deletedAdviserAvailableDates);
            }


            foreach (var date in model.AdviserAvailableDates)
            {

                var adviserAvailableDate = new AdviserAvailableDate()
                {
                    AdviserId = adviser.Id,
                    DayOfWeek = date.DayOfWeek,
                    StartTime = date.StartTime.Value,
                    EndTime = date.EndTime.Value,
                };
                await _adviserAvailableDateRepository.InsertAsync(adviserAvailableDate);


                /* if (adviserAvailableDates.Any(x => x.Id == date.Id))
                 {
                     var updatedAvailableDate = adviserAvailableDates.First(x => x.Id == date.Id);
                     updatedAvailableDate.StartTime = date.StartTime;
                     updatedAvailableDate.EndTime = date.EndTime;
                     updatedAvailableDate.DayOfWeek = date.DayOfWeek;

                     _adviserAvailableDateRepository.Update(updatedAvailableDate);
                 }*/
            }
            /*  var deletedDate = adviserAvailableDates.Where(x => !model.AdviserAvailableDates.Any(date => date.Id == x.Id)).ToList();

              foreach (var date in deletedDate)
              {
                  date.IsDeleted = true;
                  _adviserAvailableDateRepository.Update(date);
              }*/

            #endregion

            #region ConsultationType
            var deletedAdviserConsultationTypes = await _adviserConsultationTypeRespositry.GetAllAsync(x => !x.IsDeleted && x.AdviserId == adviser.Id);

            if (deletedAdviserConsultationTypes != null && deletedAdviserConsultationTypes.Any())
            {
                foreach (var type in deletedAdviserConsultationTypes)
                {
                    type.IsDeleted = true;
                }
                _adviserConsultationTypeRespositry.UpdateRange(deletedAdviserConsultationTypes);
            }

            foreach (var type in model.AdviserConsultationTypes)
            {


                var adviserConsultationType = new AdviserConsultationType()
                {
                    AdviserId = adviser.Id,
                    Title = type.Title,
                    Price = type.Price.Value,
                    IsPublished = true,
                };
                await _adviserConsultationTypeRespositry.InsertAsync(adviserConsultationType);


                /*  if (adviserConsultationTypes.Any(x => x.Id == type.Id))
                  {
                      var updatedConsultationType = adviserConsultationTypes.First(x => x.Id == type.Id);
                      updatedConsultationType.Price = type.Price.Value;
                      updatedConsultationType.Title = type.Title;


                      _adviserConsultationTypeRespositry.Update(updatedConsultationType);
                  }*/
            }
            /*   var deletedConsultationType = adviserConsultationTypes.Where(x => !model.AdviserConsultationTypes.Any(type => type.Id == x.Id)).ToList();

               foreach (var type in deletedConsultationType)
               {
                   type.IsDeleted = true;
                   _adviserConsultationTypeRespositry.Update(type);
               }*/
            #endregion

            await _adviserRepository.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }


        #endregion

        #region Filter
        public async Task<Result<AdminSideFilterAdvisersViewModel>> FilterAdvisers(AdminSideFilterAdvisersViewModel model)
        {
            if (model is null)
            {
                return Result.Failure<AdminSideFilterAdvisersViewModel>(ErrorMessages.ProcessFailedError);
            }

            var conditions = Filter.GenerateConditions<Adviser>();

            await _adviserRepository.FilterAsync(model, conditions, AdviserMapper.MapAdminSideAdviserViewModel, orderBy: ac => ac.Priority);

            var userIds = model.Entities.Select(n => n.UserId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(card => card
                .MapFrom(usersInfo.Value.First(n => n.Id == card.UserId)))
                .ToList();


            return model;

        }

        public async Task<Result<ClientSideFilterAdvisersViewModel>> ClientSideFilterAdvisers(ClientSideFilterAdvisersViewModel model)
        {
            if (model is null)
            {
                return Result.Failure<ClientSideFilterAdvisersViewModel>(ErrorMessages.ProcessFailedError);
            }

            var conditions = Filter.GenerateConditions<Adviser>();

            conditions.Add(x => x.IsPublished);

            await _adviserRepository.FilterAsync(model, conditions, AdviserMapper.MapClientSideAdviserViewModel, orderBy: ac => ac.Priority);

            if(model.Entities== null || !model.Entities.Any())
                return Result.Failure<ClientSideFilterAdvisersViewModel>(ErrorMessages.ThereIsNoAdvirsers);

            var userIds = model.Entities.Select(n => n.UserId).ToList();

            #region UserName And UserProfile
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(card => card
                .MapFrom(usersInfo.Value.First(n => n.Id == card.UserId)))
                .ToList();
            #endregion

            #region Master Bio
            var mastersBio = await _masterService.GetMasterBioByUserId(userIds);
            model.Entities = model.Entities
                 .Select(master => master
                 .MapFrom(mastersBio.Value.First(n => n.UserId == master.UserId)))
                 .ToList();
            #endregion


            return model;
        }

        #endregion

        #region Get
        public async Task<Result<AdminSideUpsertAdviserViewModel>> GetForEditAdviserAsync(int id)
        {
            if (id == null || id <= 0) return Result.Failure<AdminSideUpsertAdviserViewModel>(ErrorMessages.NullValue);

            var adviser = await _adviserRepository.GetAdviserWithDatesAndTypesAsync(id);

            if (adviser == null) return Result.Failure<AdminSideUpsertAdviserViewModel>(ErrorMessages.NullValue);

            var adminSideAdviserViewModel = new AdminSideUpsertAdviserViewModel().MapFrom(adviser);

            var userIds = new List<int>();
            userIds.Add(adminSideAdviserViewModel.UserId);
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            if (usersInfo.IsSuccess)
                adminSideAdviserViewModel.AdviserFullName = usersInfo.Value.FirstOrDefault().UserName;

            return adminSideAdviserViewModel;
        }
        public async Task<Result<ClientSideAdviserViewModel>> GetAdviserAsync(int id)
        {
            if (id == null || id <= 0) return Result.Failure<ClientSideAdviserViewModel>(ErrorMessages.NullValue);

            var adviser = await _adviserRepository.GetAdviserWithDatesAndTypesAsync(id);

            if (adviser == null) return Result.Failure<ClientSideAdviserViewModel>(ErrorMessages.NullValue);

            var clientSideAdviserViewModel = new ClientSideAdviserViewModel().MapFrom(adviser);

            var userIds = new List<int>();
            userIds.Add(clientSideAdviserViewModel.UserId);
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            if (usersInfo.IsSuccess)
                clientSideAdviserViewModel.AdviserUserName = usersInfo.Value.FirstOrDefault().UserName;

            return clientSideAdviserViewModel;

        }

        #endregion

        #region Delete
        public async Task<Result> DeleteAdviserAsync(int id)
        {
            if (id == null || id <= 0) return Result.Failure(ErrorMessages.NullValue);

            await _adviserRepository.SoftDelete(id);
            await _adviserRepository.SaveAsync();

            var adviserDeletedEvent = new AdviserDeletedEvent(id);
            await _mediatorHandler.PublishEvent(adviserDeletedEvent);

            return Result.Success();
        }

        public async Task<Result<Adviser>> GetAdviserById(int id)
        {
            if(id <= 0 ) return Result.Failure<Adviser>(ErrorMessages.NullValue);

            var adviser = await _adviserRepository.GetByIdAsync(id);

            return adviser;

        }


        #endregion

        #endregion


    }
}
