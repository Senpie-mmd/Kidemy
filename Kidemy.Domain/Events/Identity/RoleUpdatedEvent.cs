using MediatR;

namespace Kidemy.Domain.Events.Identity
{
    public record RoleUpdatedEvent(

            int Id,
            string NewTitle,
            string OldTitle,
            string NewUniqueName,
            string OldUniqueName,
            List<int>? NewPermissionIds,
            List<int>? OldPermissionIds) : INotification;


}
