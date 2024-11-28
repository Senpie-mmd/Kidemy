using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class AdminSideFilterTicketViewModel : BasePaging<AdminSideTicketViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Message")]
        public string? Message { get; set; }

        public int? OwnerUserId { get; set; }

        [Display(Name = "Priority")]
        public TicketPriority? Priority { get; set; }

        [Display(Name = "Section")]
        public TicketSection? Section { get; set; }

        [Display(Name = "Status")]
        public TicketStatus? Status { get; set; }

        public bool? IsClosed { get; set; }
    }
}
