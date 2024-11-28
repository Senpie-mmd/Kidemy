using MediatR;

namespace Kidemy.Domain.Events.SocialMedia
{
    public record SocialMediaCreateEvent(

        int Id,
        int Priority,
        string Title,
        string Link,
        string ImageName
        ) : INotification;

}
