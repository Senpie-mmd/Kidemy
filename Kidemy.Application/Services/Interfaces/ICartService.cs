using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<Result> AddItemToCurrentCart(AddToCartViewModel model);
        Task<Result<bool>> CanAddToCart(AddToCartViewModel model);
        Task<Result> ClearCurrentCartItems(int userId);
        Task<Result<CartViewModel>> GetCurrentCartAsync(int userId, string? discountCode = null);
        Task<Result<WalletTransactionDetailsViewModel>> ProcessCartPayment(ProcessCartPaymentViewModel model);
        Task<Result> RemoveItemFromCurrentCart(RemoveFromCartViewModel model);
    }
}