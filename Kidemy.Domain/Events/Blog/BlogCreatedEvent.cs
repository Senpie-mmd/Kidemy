using Kidemy.Domain.Enums.Blog;
using MediatR;

namespace Kidemy.Domain.Events.Blog
{
    public record BlogCreatedEvent(
        string Title,
        string Body,
        string Slug,
        bool IsPublished,
        string ImageName,
        int AuthorId
        ) : INotification;
}