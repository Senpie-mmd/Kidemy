using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Ticket
{
    public enum TicketPriority
    {
        [Display(Name = "Low")]
        Low,

        [Display(Name = "Medium")]
        Medium,

        [Display(Name = "High")]
        High,

        [Display(Name = "VeryHigh")]
        VeryHigh
    }
}
