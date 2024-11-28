using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record MasterContractCreatedEvent
         (
        int Id,
        string Title,
        string FileName,
        bool IsPublished
        ) : INotification;
}