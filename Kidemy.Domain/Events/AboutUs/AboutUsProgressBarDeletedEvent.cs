using MediatR;

namespace Kidemy.Domain.Events.AboutUs
{
    public record AboutUsProgressBarDeletedEvent(
        int id
        ) : INotification;
}
