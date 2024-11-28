using MediatR;

namespace Kidemy.Domain.Events.Blog
{
    public record BlogDeletedEvent(
        int Id): INotification;

}
