using MediatR;

namespace Kidemy.Domain.Events.Page
{
    public record PageDeletedEvent(
        int Id) : INotification;

}
