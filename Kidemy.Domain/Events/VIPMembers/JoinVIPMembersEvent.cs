using MediatR;

namespace Kidemy.Domain.Events.VIPMembers
{
    public record JoinVIPMembersEvent(
        int UserId,
        int VIPPlanId,
        DateTime Title,
        DateTime Address
         ) : INotification;

}
