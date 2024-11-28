using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Events.User;
using Kidemy.Domain.Statics;
using MediatR;

namespace Kidemy.Application.Handlers.Master
{
    public class MasterHandler : INotificationHandler<UserUpdatedByAdminEvent>,
        INotificationHandler<UserCreatedEvent>
    {
        private readonly IMasterService _masterService;

        public MasterHandler(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public async Task Handle(UserUpdatedByAdminEvent notification, CancellationToken cancellationToken)
        {
            if (notification.NewRoleIds?.Contains(Roles.Master) ?? false)
            {
                var result = await _masterService.IsExistMasterByIdAsync(notification.Id);

                if (result.IsSuccess && result.Value == false)
                {
                    await _masterService.CreateAsync(notification.Id);
                }
            }
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.NewRoleIds?.Contains(Roles.Master) ?? false)
            {
                var result = await _masterService.IsExistMasterByIdAsync(notification.Id);

                if (result.IsSuccess && result.Value == false)
                {
                    await _masterService.CreateAsync(notification.Id);
                }
            }
        }
    }
}
