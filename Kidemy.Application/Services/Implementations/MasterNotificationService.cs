using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.MasterNotification;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Events.MasterNotification;
using Kidemy.Domain.Events.Page;
using Kidemy.Domain.Interfaces.MasterNotification;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Models.MasterNotification;
using Kidemy.Domain.Models.Page;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Application.Services.Implementations
{
    public class MasterNotificationService : IMasterNotificationService
    {
        #region Field
        private readonly IMasterNotificationRepository _masterNotificationRepository;
        private readonly ISmsSenderSevice _smsSenderService;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public MasterNotificationService(
            IMasterNotificationRepository masterNotificationRepository,
            IMasterService masterService,
            IMediatorHandler mediatorHandler, ISmsSenderSevice smsSenderSevice,
            IUserService userService,
             IStringLocalizer localizer)
        {
            _masterNotificationRepository = masterNotificationRepository;
            _masterService = masterService;
            _mediatorHandler = mediatorHandler;
            _smsSenderService = smsSenderSevice;
            _userService = userService;
            _localizer = localizer;
        }
        #endregion

        public async Task<Result> SendMasterNotification(SendMasterNotificationViewModel model)
        {
            string message = model.Message;

            List<int> MasterIds = new List<int>();
            List<string> Numbers = new List<string>();

            if (model.IsAllMaster)
            {
                var IdResult = await _masterService.GetMastersIds();

                if (IdResult.IsFailure)
                {
                    return Result.Failure(IdResult.Message);
                }
                var MobileResult = await _userService.GetUsersProfileDetails(IdResult.Value);

                MasterIds = IdResult.Value;
                Numbers = MobileResult.Select(m => m.Mobile).ToList();
            }
            else
            {
                MasterIds = model.MasterIds.Split("-").Select(n => int.Parse(n)).ToList();

                if (MasterIds.Count() is not 0)
                {
                    var MobileResult = await _userService.GetUsersProfileDetails(MasterIds);

                    Numbers = MobileResult.Select(m => m.Mobile).ToList();

                }
                else
                {
                    return Result.Failure(ErrorMessages.FailedOperationError);
                }

            }

            await _smsSenderService.SendSms(Numbers.ToArray(), _localizer["A notification has been sent to you"].ToString()); 


            MasterNotification masterNotification = new()
            {
                SenderId = model.SenderId,
                Message = message,
                DemoVideoFileName = model.DemoVideoFileName,
                CreatedDateOnUtc = DateTime.UtcNow,
                Masters = MasterIds.Select(item => new NotificationMasterMapping()
                {
                    MasterId = item,

                }).ToList(),
            };

            await _masterNotificationRepository.InsertAsync(masterNotification);
            await _masterNotificationRepository.SaveAsync();


            var masterNotificationCreatedEvent = new MasterNotificationCreatedEvent(
                message,
                MasterIds
                );

            await _mediatorHandler.PublishEvent(masterNotificationCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<List<ClientSideMasterNotificationViewModel>>> GetMasterNotificationsAsync(int masterId)
        {

            var notificationMasters = await _masterNotificationRepository
                .GetAllAsync(n => n.Masters.Any(master => master.MasterId == masterId));

            if (notificationMasters is null || !notificationMasters.Any())
            {
                return new List<ClientSideMasterNotificationViewModel>();
            }
            var models = notificationMasters.Select(s => new ClientSideMasterNotificationViewModel().MapFrom(s)).ToList();

            return models;

        }

        public async Task<Result<FilterMasterNotificationViewModel>> FilterAsync(FilterMasterNotificationViewModel model)
        {
            if (model == null) return Result.Failure<FilterMasterNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<MasterNotification>();

            await _masterNotificationRepository.FilterAsync(model, filterConditions, MasterNotificationMapper.AdminMapMasterNotificationViewModel);

            return model;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _masterNotificationRepository.SoftDelete(id);
            await _masterNotificationRepository.SaveAsync();


            MasterNotificationDeletedEvent masterNotificationDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(masterNotificationDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result> UpdateAsync(AdminSideUpdateMasterNotificationViewModel model)
        {

            var masterNotification = await _masterNotificationRepository.GetByIdAsync(model.Id);

            if (masterNotification is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedMasterNotification = masterNotification?.DeepCopy<MasterNotification>();

            masterNotification.Message = model.Message;
            masterNotification.DemoVideoFileName = model.DemoVideoFileName;
            masterNotification.UpdatedDateOnUtc = DateTime.UtcNow;

            _masterNotificationRepository.Update(masterNotification);
            await _masterNotificationRepository.SaveAsync();

            MasterNotificationUpdatedEvent masterNotificationUpdatedEvent = new(
            masterNotification.Id,
            masterNotification.Message,
            copiedMasterNotification.Message,
            masterNotification.DemoVideoFileName,
            copiedMasterNotification.DemoVideoFileName);

            await _mediatorHandler.PublishEvent(masterNotificationUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<AdminSideUpdateMasterNotificationViewModel>> GetMasterNotificationForEditById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateMasterNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var masterNotification = await _masterNotificationRepository.GetByIdAsync(id);

            if (masterNotification is null) return Result.Failure<AdminSideUpdateMasterNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateMasterNotificationViewModel().MapFrom(masterNotification);

            return model;
        }
    }
}
