using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.MasterNotification;
using Kidemy.Application.ViewModels.Notification;
using Kidemy.Domain.Events.MasterNotification;
using Kidemy.Domain.Events.Notification;
using Kidemy.Domain.Interfaces.Notification;
using Kidemy.Domain.Models.MasterNotification;
using Kidemy.Domain.Models.Notification;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        #region Field
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ISmsSenderSevice _smsSenderService;
        private readonly IMediatorHandler _mediatorHandler;
        #endregion

        #region Ctor
        public NotificationService(
            INotificationRepository notificationRepository,
            IUserService userService,
            IEmailService emailService,
            ISmsSenderSevice smsSenderService,
            IMediatorHandler mediatorHandler)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
            _emailService = emailService;
            _smsSenderService = smsSenderService;
            _mediatorHandler = mediatorHandler;
        }
        #endregion
        public async Task<Result> SendNotification(SendNotificationViewModel model)
        {
            string title = model.Subject;
            string message = model.Message;

            List<int> UserIds = new List<int>();
            List<string> Emails = new List<string>();
            List<string> Numbers = new List<string>();

            if (model.IsAllUser)
            {
                var EmailResult = await _userService.GetUsersEmails();
                var MobileResult = await _userService.GetUsersNumbers();
                var IdResult = await _userService.GetUsersIds();

                Emails = EmailResult.Value;
                Numbers = MobileResult.Value;
                UserIds = IdResult.Value;
            }
            else
            {
                UserIds = model.UserIds.Split("-").Select(n => int.Parse(n)).ToList();
                if (UserIds.Count() is not 0)
                {
                    foreach (var item in UserIds)
                    {
                        var email = await _userService.GetUserEmailByIdAsync(item);
                        var number = await _userService.GetUserMobileByIdAsync(item);

                        Emails.Add(email.Value);
                        Numbers.Add(number.Value);
                    }
                }
                else
                {
                    return Result.Failure(ErrorMessages.FailedOperationError);
                }

            }

            await _emailService.SendEmailAsync(Emails, title, message);
            await _smsSenderService.SendSms(Numbers.ToArray(), message);

            Notification notification = new()
            {
                SenderId = model.SenderId,
                Subject = title,
                Message = message,
                CreatedDateOnUtc = DateTime.UtcNow,
                Users = UserIds.Select(item => new NotificationUserMapping()
                {
                    UserId = item,
                }).ToList(),
            };

            await _notificationRepository.InsertAsync(notification);
            await _notificationRepository.SaveAsync();


            var notificationCreatedEvent = new NotificationCreatedEvent(
                title,
                message,
                UserIds
                );

            await _mediatorHandler.PublishEvent(notificationCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<FilterNotificationViewModel>> FilterAsync(FilterNotificationViewModel model)
        {
            if (model == null) return Result.Failure<FilterNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Notification>();

            if (!string.IsNullOrEmpty(model.Subject))
            {
                filterConditions.Add(p => p.Subject.Contains(model.Subject));
            }

            await _notificationRepository.FilterAsync(model, filterConditions, NotificationMapper.AdminMapNotificationViewModel);

            return model;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _notificationRepository.SoftDelete(id);
            await _notificationRepository.SaveAsync();


            NotificationDeletedEvent notificationDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(notificationDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result> UpdateAsync(AdminSideUpdateNotificationViewModel model)
        {

            var notification = await _notificationRepository.GetByIdAsync(model.Id);

            if (notification is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedNotification = notification?.DeepCopy<Notification>();

            notification.Subject = model.Subject;
            notification.Message = model.Message;
            notification.UpdatedDateOnUtc = DateTime.UtcNow;

            _notificationRepository.Update(notification);
            await _notificationRepository.SaveAsync();

            NotificationUpdatedEvent notificationUpdatedEvent = new(
            notification.Id,
            notification.Message,
            copiedNotification.Message);

            await _mediatorHandler.PublishEvent(notificationUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<AdminSideUpdateNotificationViewModel>> GetNotificationForEditById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var notification = await _notificationRepository.GetByIdAsync(id);

            if (notification is null) return Result.Failure<AdminSideUpdateNotificationViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateNotificationViewModel().MapFrom(notification);

            return model;
        }
    }
}
