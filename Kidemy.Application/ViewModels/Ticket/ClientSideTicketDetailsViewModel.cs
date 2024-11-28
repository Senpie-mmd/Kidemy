using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Ticket;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class ClientSideTicketDetailsViewModel : BaseEntityViewModel<int>
    {
        public string Title { get; set; }

        public int OwnerUserId { get; set; }

        public bool IsClosed { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketSection Section { get; set; }

        public List<ClientSideTicketMessageDetailsViewModel> Messages { get; set; }

    }
}
