using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Ticket
{
    public enum TicketStatus
    {
        [Display(Name = "PendingUser")]
        PendingUser,

        [Display(Name = "PendingAdmin")]
        PendingAdmin,

        [Display(Name = "Answered")]
        Answered,

        [Display(Name = "Closed")]
        Closed
    }
}
