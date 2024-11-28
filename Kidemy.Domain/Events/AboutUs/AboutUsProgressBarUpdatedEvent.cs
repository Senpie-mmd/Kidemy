using MediatR;

namespace Kidemy.Domain.Events.AboutUs
{
    public record AboutUsProgressBarUpdatedEvent(
        int id,
        string prevTitle,
        int prevPercent,
        string newTitle,
        int newPercent
        ) : INotification;
}
