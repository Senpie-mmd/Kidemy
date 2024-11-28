using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class AddTicketMessageViewModel
    {
        public int TicketId { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Message { get; set; }

        public int SenderId { get; set; }
    }
}