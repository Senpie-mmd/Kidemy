using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Order;

public class ClientSideOrderDetailsViewModel : BaseEntityViewModel<int>
{
    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal? DiscountedTotalAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime CreatedDateOnUtc { get; set; }

    public List<ClientSideOrderItemViewModel> Items { get; set; }
}
