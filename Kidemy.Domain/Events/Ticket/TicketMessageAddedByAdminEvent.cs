using MediatR;

namespace Kidemy.Domain.Events.Ticket
{
    public record TicketMessageAddedByAdminEvent(

        int TicketId,
        int SenderUserId,
        string Message

        ) : INotification;

}
