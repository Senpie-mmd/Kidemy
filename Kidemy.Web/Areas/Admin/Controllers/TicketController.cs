using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Ticket;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageTicket")]
    public class TicketController : BaseAdminController
    {
        #region Fields

        private readonly ITicketService _ticketService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public TicketController(ITicketService ticketService, IStringLocalizer<SharedResource> localizer)
        {
            _ticketService = ticketService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        [Permission("TicketList")]
        public async Task<IActionResult> List(AdminSideFilterTicketViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _ticketService.FilterForAdminAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        [Permission("CreateTicket")]
        public IActionResult Create()
        {
            return View(new CreateTicketViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("CreateTicket")]
        public async Task<IActionResult> Create(CreateTicketViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _ticketService.CreateByAdminAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction("List", "Ticket", new { area = "Admin" });
        }

        [HttpGet]
        [Permission("UpdateTicket")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _ticketService.GetForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(new UpdateTicketViewModel());
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("UpdateTicket")]
        public async Task<IActionResult> Update(UpdateTicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

                return RedirectToAction("Update", new { id = model.Id });
            }

            var result = await _ticketService.UpdateTicketAsync(model);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        [Permission("TicketDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _ticketService.GetDetailsForAdminAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("List", "Ticket", new { area = "Admin" });
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("TicketDetails")]
        public async Task<IActionResult> Details(AddTicketMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

                return RedirectToAction("Details", new { id = model.TicketId });
            }

            model.SenderId = HttpContext.User.GetUserId();

            var result = await _ticketService.AddTicketMessageByAdmin(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("List", "Ticket", new { area = "Admin" });

            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return RedirectToAction("Details", new { id = model.TicketId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseTicket(int id)
        {
            if (id is < 1)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return RedirectToAction(nameof(List));
            }
            var result = await _ticketService.CloseTicket(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
