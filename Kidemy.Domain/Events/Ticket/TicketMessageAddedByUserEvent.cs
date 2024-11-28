using MediatR;

namespace Kidemy.Domain.Events.Ticket
{
    public record TicketMessageAddedByUserEvent(
        int TicketId,
        int SenderUserId,
        string Message
        ) : INotification;
}
