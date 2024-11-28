using MediatR;

namespace Kidemy.Domain.Events.Banner
{
    public record BannerDeletedEvent
     (
        int Id
        ) : INotification;
}
