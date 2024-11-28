using Kidemy.Domain.Enums.Ticket;
using MediatR;

namespace Kidemy.Domain.Events.Ticket
{
    public record TicketUpdatedEvent(
        int id,
        TicketStatus PrevStatus,
        TicketStatus NewStatus,
        TicketPriority PrevPriority,
        TicketPriority NewPriority,
        TicketSection PrevSection,
        TicketSection NewSection
        ) : INotification;
}
