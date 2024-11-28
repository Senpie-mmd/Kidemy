using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Models.WithdrawRequest;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardPendingWithdrawRequestsViewComponent : ViewComponent
    {
        private readonly IWithdrawRequestService _withdrawRequestService;

        public AdminDashboardPendingWithdrawRequestsViewComponent(IWithdrawRequestService withdrawRequestService)
        {
            _withdrawRequestService = withdrawRequestService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new FilterWithdrawRequestViewModel()
            {
                Status = WithdrawRequestStatus.Pending,
                TakeEntity = 5
            };
            var result = await _withdrawRequestService.FilterWithdrawRequestAsync(model);

            if (result.IsSuccess)
                model = result.Value;

            return View("AdminDashboardPendingWithdrawRequests", model);
        }
    }
}
