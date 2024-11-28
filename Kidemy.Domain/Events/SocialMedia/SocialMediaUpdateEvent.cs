using MediatR;

namespace Kidemy.Domain.Events.SocialMedia
{
    public record SocialMediaUpdateEvent(

        int Id,
        int NewPriority,
        string NewTitle,
        string NewLink,
        string NewImageName,
        int OldPriority,
        string OldTitle,
        string OldLink,
        string OldImageName
        ) : INotification;

}
