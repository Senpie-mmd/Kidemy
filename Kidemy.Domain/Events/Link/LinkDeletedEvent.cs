using MediatR;

namespace Kidemy.Domain.Events.Link
{
    public record LinkDeletedEvent(
        int Id
        ) : INotification;
}
