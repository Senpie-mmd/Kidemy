using MediatR;

namespace Kidemy.Domain.Events.Identity
{
    public record RoleDeletedEvent
        (
            int Id

        ) : INotification;

}
