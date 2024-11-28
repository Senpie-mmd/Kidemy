using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Ticket
{
    public class TicketMessage : AuditBaseEntity<int>
    {
        public int TicketId { get; set; }

        public string Message { get; set; }

        public int SenderId { get; set; }

        public bool SeenByClient { get; set; }

        public bool IsDeleted { get; set; }

        public Ticket Ticket { get; set; }
    }
}
