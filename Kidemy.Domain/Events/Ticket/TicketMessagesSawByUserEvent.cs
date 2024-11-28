using MediatR;

namespace Kidemy.Domain.Events.Ticket
{
    public record TicketMessagesHaveBeenSeenByUserEvent(int ticketId) : INotification;
}
