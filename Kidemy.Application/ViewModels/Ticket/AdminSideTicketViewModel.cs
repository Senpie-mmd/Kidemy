using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Ticket;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class AdminSideTicketViewModel : BaseEntityViewModel<int>
    {
        public string Title { get; set; }

        public int OwnerUserId { get; set; }

        public string UserFullName { get; set; }

        public bool IsClosed { get; set; }

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketSection Section { get; set; }

        public DateTime CreateDateOnUtc { get; set; }
    }
}
