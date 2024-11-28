using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Ticket
{
    public class AdminSideTicketMessageDetailsViewModel : BaseEntityViewModel<int>
    {
        public string Message { get; set; }

        public int SenderId { get; set; }

        public string? AdminFullName { get; set; }

        public bool SeenByClient { get; set; }

        public DateTime CreateDateOnUtc { get; set; }

    }
}
