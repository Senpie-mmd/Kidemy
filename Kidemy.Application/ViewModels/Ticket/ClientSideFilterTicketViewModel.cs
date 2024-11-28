using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class ClientSideFilterTicketViewModel : BasePaging<ClientSideTicketViewModel>
    {
        public int? OwnerUserId { get; set; }
    }
}
