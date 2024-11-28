using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Ticket;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;
namespace Kidemy.Application.ViewModels.Ticket
{
    public class CreateTicketViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }

        [Display(Name = "OwnerUserId")]
        public int? OwnerUserId { get; set; }

        public string? TempOwnerFullName { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public TicketPriority? Priority { get; set; }

        [Display(Name = "Section")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public TicketSection? Section { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        public string? HiddenUserName { get; set; }
    }
}
