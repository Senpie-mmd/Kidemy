using MediatR;

namespace Kidemy.Domain.Events.AboutUs
{
    public record AboutUsProgressBarCreatedEvent(
        string title,
        int percent
        ) : INotification;
}
