using Kidemy.Domain.Enums.Banner;
using MediatR;

namespace Kidemy.Domain.Events.Banner
{
    public record BannerCreatedEvent
    (
        int Id,
        string ImageName,
        BannerPlace BannerPlace,
        string? URL,
        bool IsPublished

    ) : INotification;
}
