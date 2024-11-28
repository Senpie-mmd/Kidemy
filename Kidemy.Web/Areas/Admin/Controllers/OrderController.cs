using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Order;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageOrders")]
    public class OrderController : BaseAdminController
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Contsructor

        public OrderController(IOrderService orderService, IStringLocalizer<SharedResource> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        #endregion

        #region Methods

        #region List

        [Permission("OrdersList")]
        public async Task<IActionResult> List(AdminSideFilterOrderViewModel filter)
        {
            var result = await _orderService.FilterForAdminAsync(filter);

            return result.IsSuccess ? View(result.Value) : View(filter);
        }

        #endregion

        #region Order details

        [Permission("OrderDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var orderDetailsResult = await _orderService.GetOrderDetailsForAdminAsync(id);

            if (orderDetailsResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[orderDetailsResult.Message].ToString();
                return View();
            }

            var model = orderDetailsResult.Value;

            return PartialView("_Details", model);
        }

        #endregion

        #endregion
    }
}
