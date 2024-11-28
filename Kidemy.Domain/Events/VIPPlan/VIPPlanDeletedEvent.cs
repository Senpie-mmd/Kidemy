using MediatR;

namespace Kidemy.Domain.Events.VIPPlan
{
    public record VIPPlanDeletedEvent(
          int Id) : INotification;
}
