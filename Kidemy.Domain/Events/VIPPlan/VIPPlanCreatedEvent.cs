using MediatR;

namespace Kidemy.Domain.Events.VIPPlan
{
    public record VIPPlanCreatedEvent
        (
        string Title,
        int DurationDay,
        decimal Price,
        bool IsPublished
        ) : INotification;
}