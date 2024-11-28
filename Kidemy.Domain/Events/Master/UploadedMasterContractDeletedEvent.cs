using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record UploadedMasterContractDeletedEvent
        (
        int Id) : INotification;
}
