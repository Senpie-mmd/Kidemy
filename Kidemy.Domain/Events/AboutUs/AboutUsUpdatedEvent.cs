using MediatR;

namespace Kidemy.Domain.Events.AboutUs
{
    public record AboutUsUpdatedEvent(
        int id,
        string? prePageTitle,
        string? prevTitle,
        string? prevDescription,
        string? prevOurGoal,
        string? prevOurGoalTitle,
        string? prevOurGoalDescription,
        string? prevOurGoalFeatures,
        string? newPageTitle,
        string? newTitle,
        string? newDescription,
        string? newOurGoal,
        string? newOurGoalTitle,
        string? newOurGoalDescription,
        string? newOurGoalFeatures
        ) : INotification;
}
