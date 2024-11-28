using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Ticket;

namespace Kidemy.Domain.Models.Ticket
{
    public class Ticket : AuditBaseEntity<int>
    {
        public string Title { get; set; }

        public int OwnerUserId { get; set; }

        public bool IsClosed { get; set; }

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketSection Section { get; set; }

        public ICollection<TicketMessage> Messages { get; set; }
    }
}
