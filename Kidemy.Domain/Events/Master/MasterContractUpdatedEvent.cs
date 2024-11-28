using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record MasterContractUpdatedEvent
         (
        int Id,
        string NewTitle,
        string OldTitle,
        string NewFileName,
        string OldFileName,
        bool NewIsPublished,
        bool OldIsPublished
        ) : INotification;
}
