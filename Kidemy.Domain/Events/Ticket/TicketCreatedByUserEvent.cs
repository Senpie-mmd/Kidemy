using Kidemy.Domain.Enums.Ticket;
using Kidemy.Domain.Models.Ticket;
using MediatR;

namespace Kidemy.Domain.Events.Ticket
{
    public record TicketCreatedByUserEvent(

        int Id,
        string Title,
        int OwnerUserId,
        TicketStatus Status,
        TicketPriority Priority,
        TicketSection Section,
        ICollection<TicketMessage> Message

        ) : INotification;

}
