using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Order;

public class ClientSideOrderViewModel : BaseEntityViewModel<int>
{
    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal? DiscountedTotalAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime CreatedDateOnUtc { get; set; }
}
