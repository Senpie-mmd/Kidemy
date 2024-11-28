using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Order;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<WalletTransactionDetailsViewModel>> ConfirmOrderPaymentAsync(int orderId);
        Task<Result<AdminSideFilterOrderViewModel>> FilterForAdminAsync(AdminSideFilterOrderViewModel filter);
        Task<Result<ClientSideFilterOrderViewModel>> FilterForClientAsync(ClientSideFilterOrderViewModel filter);
        Task<Result<AdminSideOrderDetailsViewModel>> GetOrderDetailsForAdminAsync(int orderId);
        Task<Result<ClientSideOrderDetailsViewModel>> GetOrderDetailsForClientAsync(int orderId, int userId);
        Task<Result<int>> RegisterOrderAsync(CartViewModel cart);
    }
}