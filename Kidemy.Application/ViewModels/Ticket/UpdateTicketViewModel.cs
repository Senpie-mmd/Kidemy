using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Ticket;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;
namespace Kidemy.Application.ViewModels.Ticket
{
    public class UpdateTicketViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public TicketPriority Priority { get; set; }

        [Display(Name = "Section")]
        public TicketSection Section { get; set; }
    }
}
