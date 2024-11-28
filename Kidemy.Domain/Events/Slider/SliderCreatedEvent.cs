using Kidemy.Domain.Enums.Slider;
using MediatR;

namespace Kidemy.Domain.Events.Slider
{
    public record SliderCreatedEvent
     (
        int Id,
        string Title,
        string Description,
        string ImageName,
        int Priority,
        SliderPlace SliderPlace,
        string URL,
        bool IsPublished
        ) : INotification;
}
