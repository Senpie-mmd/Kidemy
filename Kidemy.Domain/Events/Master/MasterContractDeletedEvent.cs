using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record MasterContractDeletedEvent(
        int Id
        ) : INotification;
}
