using Kidemy.Application.Security;

namespace Kidemy.Application.ViewModels.Cart
{
    public class ProcessCartPaymentViewModel
    {
        public string? DiscountCode { get; set; }

        public bool UseFromWalletBalance { get; set; }

        public int UserId { get; set; }

        public string? UserIP { get; set; }
    }
}
