using Kidemy.Domain.Enums.DynamicText;
using MediatR;

namespace Kidemy.Domain.Events.DynamicText
{
    public record DynamicTextUpdatedEvent(
        int Id,
        string NewText,
        string OldText,
        DynamicTextPosition NewPosition,
        DynamicTextPosition OldPosition) : INotification;

}
