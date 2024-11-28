using Kidemy.Domain.Enums.Page;
using MediatR;

namespace Kidemy.Domain.Events.Page
{
    public record PageUpdatedEvent(
        int Id,
        string NewTitle,
        string OldTitle,
        string NewBody,
        string OldBody,
        string NewSlug,
        string OldSlug,
        bool NewIsPublished,
        bool OldIsPublished,
        LinkPlace NewLinkPlace,
        LinkPlace OldLinkPlace) : INotification;

}
