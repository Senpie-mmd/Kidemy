using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Ticket
{
    public enum TicketSection
    {
        [Display(Name = "Financial")]
        Financial,

        [Display(Name = "Technical")]
        Technical,

        [Display(Name = "Support")]
        Support
    }
}
