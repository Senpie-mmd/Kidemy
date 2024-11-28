using Kidemy.Domain.Enums.Slider;
using MediatR;

namespace Kidemy.Domain.Events.Slider
{
    public record SliderUpdatedEvent
     (
        int Id,
        string NewTitle,
        string OldTitle,
        string NewDescription,
        string OldDescription,
        string NewImageName,
        string OldImageName,
        int NewPriority,
        int OldPriority,
        SliderPlace NewSliderPlace,
        SliderPlace OldSliderPlace,
        string NewURL,
        string OldURL,
        bool NewIsPublished,
        bool OldIsPublished
        ) : INotification;
}
