using MediatR;

namespace Kidemy.Domain.Events.Identity
{
    public record RoleCreateEvent(

        int Id,
        string Title,
        string UniqueName,
        List<int>? PermissionId

            ) : INotification;

}
