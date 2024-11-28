using Kidemy.Domain.Enums.Page;
using MediatR;

namespace Kidemy.Domain.Events.Page
{
    public record PageCreatedEvent(
        string Title,
        string Body,
        string Slug,
        bool IsPublished,
        LinkPlace LinkPlace) : INotification;
}