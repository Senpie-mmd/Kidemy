using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class SettlementTransactionController : BaseUserPanelController
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ISettlementTransactionService _settlementTransactionService;

        #endregion

        #region Constructor
        public SettlementTransactionController(ISettlementTransactionService settlementTransactionService, IStringLocalizer<SharedResource> localizer)
        {
            _settlementTransactionService = settlementTransactionService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [HttpGet("settlement-list")]
        public async Task<IActionResult> SettlementTransactionList(ClientSideFilterSettlementTransactionViewModel filter)
        {
            filter.UserId = User.GetUserId();

            var result = await _settlementTransactionService.ClientSideFilterSettlementTransactionsAsync(filter);

            if (result.IsSuccess)
                return View(result.Value);

            TempData[ErrorMessage] = result.Message;
            return RedirectToAction("Index");

        }

        #endregion
    }
}
