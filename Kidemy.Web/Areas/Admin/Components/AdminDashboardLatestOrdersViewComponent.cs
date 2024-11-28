using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardLatestOrdersViewComponent : ViewComponent
    {
        #region Fields

        private readonly IOrderService _orderService;

        #endregion

        #region Constructor

        public AdminDashboardLatestOrdersViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _orderService.FilterForAdminAsync(new Application.ViewModels.Order.AdminSideFilterOrderViewModel
            {
                TakeEntity = 5, 
            });

            return View("AdminDashboardLatestOrders", model.Value);
        }

        #endregion

    }
}
