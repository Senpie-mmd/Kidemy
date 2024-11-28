using Kidemy.Domain.Enums.Link;
using MediatR;

namespace Kidemy.Domain.Events.Link
{
    public record LinkUpdatedEvent(
        int id,
        string prevTitle,
        string newTitle,
        string prevAddress,
        string newAddress,
        LinkType prevLinkType,
        LinkType newLinkType
        ) : INotification;
}
