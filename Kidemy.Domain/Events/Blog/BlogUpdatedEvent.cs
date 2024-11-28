using Kidemy.Domain.Enums.Blog;
using MediatR;

namespace Kidemy.Domain.Events.Blog
{
    public record BlogUpdatedEvent(
        int Id,
        string NewTitle,
        string OldTitle,
        string NewBody,
        string OldBody,
        string NewSlug,
        string OldSlug,
        bool NewIsPublished,
        bool OldIsPublished,
        string NewImageName,
        string OldImageName,
        int NewAuthorId,
        int OldAuthorId
        ) : INotification;

}
