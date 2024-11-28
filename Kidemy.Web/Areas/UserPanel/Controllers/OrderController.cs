using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Order;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class OrderController : BaseUserPanelController
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public OrderController(IOrderService orderService, IStringLocalizer<SharedResource> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [HttpGet("userpanel/my-orders")]
        public async Task<IActionResult> Index(ClientSideFilterOrderViewModel model)
        {
            var userId = User.GetUserId();

            model.TakeEntity = 5;
            model.UserId = userId;
            var result = await _orderService.FilterForClientAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        [HttpGet("userpanel/my-order-details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _orderService.GetOrderDetailsForClientAsync(id, User.GetUserId());

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Order", new { Area = "UserPanel" });
            }

            return View(result.Value);
        }

        #endregion
    }
}
