using Kidemy.Domain.Enums.Link;
using MediatR;

namespace Kidemy.Domain.Events.Link
{
    public record LinkCreatedEvent(
        string Title,
        string Address,
        LinkType LinkType
        ) : INotification;
}
