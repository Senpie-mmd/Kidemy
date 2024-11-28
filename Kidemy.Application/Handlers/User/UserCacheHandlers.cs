using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Statics;
using Kidemy.Domain.Events.User;
using MediatR;

namespace Kidemy.Application.Handlers.User
{
    public class UserCacheHandlers : INotificationHandler<UserUpdatedByAdminEvent>,
        INotificationHandler<UserProfileUpdatedByUserEvent>
    {
        private readonly ICacheManager _cacheManager;

        public UserCacheHandlers(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public Task Handle(UserUpdatedByAdminEvent notification, CancellationToken cancellationToken)
        {
            return _cacheManager.RemoveByPrefixAsync(CacheKeys.UserPrevCacheKey);
        }

        public Task Handle(UserProfileUpdatedByUserEvent notification, CancellationToken cancellationToken)
        {
            return _cacheManager.RemoveByPrefixAsync(CacheKeys.UserPrevCacheKey);
        }
    }
}
