using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class WalletTransactionsViewComponent : ViewComponent
    {
        private readonly IWalletService _walletService;

        public WalletTransactionsViewComponent(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var filterModel = new FilterWalletTransactionViewModel
            {
                UserId = userId,
                TakeEntity = 30
            };

            var result = await _walletService.FilterAsync(filterModel);

            if (result.IsSuccess) return View("WalletTransactions", result.Value);
            else return null;
        }
    }
}
