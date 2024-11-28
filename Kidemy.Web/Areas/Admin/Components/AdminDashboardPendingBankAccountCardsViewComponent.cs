using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Domain.Enums.BankAccount;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardPendingBankAccountCardsViewComponent : ViewComponent
    {
        private readonly IBankAccountCardService _bankAccountCardService;

        public AdminDashboardPendingBankAccountCardsViewComponent(IBankAccountCardService bankAccountCardService)
        {
            _bankAccountCardService = bankAccountCardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new FilterBankAccountCardViewModel()
            {
                Status = BankAccountCardStatus.Pending,
                TakeEntity = 5
            };
            var result = await _bankAccountCardService.FilterBankAccountCardAsync(model);

            if (result.IsSuccess)
                model = result.Value;

            return View("AdminDashboardPendingBankAccountCards", model);
        }
    }
}
