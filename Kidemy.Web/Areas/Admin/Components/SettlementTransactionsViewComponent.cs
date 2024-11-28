using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class SettlementTransactionsViewComponent : ViewComponent
    {
        private readonly ISettlementTransactionService _settlementTransactionService;

        public SettlementTransactionsViewComponent(ISettlementTransactionService settlementTransactionService)
        {
            _settlementTransactionService = settlementTransactionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var filterModel = new AdminSideFilterSettlementTransactionsViewModel()
            {
                UserId = userId,
                TakeEntity = int.MaxValue,
            };

            var result = await _settlementTransactionService.FilterSettlementTransactionsAsync(filterModel);

            return result.IsSuccess ? View("SettlementTransactions", result.Value) : null;
        }
    }
}
