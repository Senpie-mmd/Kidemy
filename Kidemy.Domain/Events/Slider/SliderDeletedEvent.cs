using MediatR;

namespace Kidemy.Domain.Events.Slider
{
    public record SliderDeletedEvent
    (
        int Id) : INotification;
}
