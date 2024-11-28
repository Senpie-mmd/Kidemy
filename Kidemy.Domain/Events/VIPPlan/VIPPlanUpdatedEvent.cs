using MediatR;

namespace Kidemy.Domain.Events.VIPPlan
{
    public record VIPPlanUpdatedEvent
        (
        int Id,
        string NewTitle,
        string OldTitle,
        int NewDurationDay,
        int OldDurationDay,
        decimal NewPrice,
        decimal OldPrice,
        bool NewIsPublished,
        bool OldIsPublished
        ) : INotification;
}