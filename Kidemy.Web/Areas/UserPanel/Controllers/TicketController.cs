using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Ticket;
using Kidemy.Domain.Statics;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class TicketController : BaseUserPanelController
    {
        #region Fields

        private readonly ITicketService _ticketService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IUserService _userServie;

        #endregion

        #region Constructor

        public TicketController(ITicketService ticketService, IStringLocalizer<SharedResource> localizer, IUserService userServie)
        {
            _ticketService = ticketService;
            _localizer = localizer;
            _userServie = userServie;
        }

        #endregion

        #region Actions

        #region List

        [HttpGet("/userpanel/tickets")]
        public async Task<IActionResult> List(ClientSideFilterTicketViewModel model)
        {
            model.OwnerUserId = HttpContext.User.GetUserId();

            var result = await _ticketService.FilterForClientAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            return View(result.Value);
        }

        #endregion

        #region Details

        [HttpGet("/userpanel/ticket/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _ticketService.GetDetailsForClientAsync(id, User.GetUserId());

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("List");
            }

            var avatarResult = await _userServie.GetUserAvatarNameByIdAsync(User.GetUserId());

            var userAvatar = SiteTools.DefaultImageName;

            if (avatarResult.IsSuccess)
            {
                userAvatar = avatarResult.Value;
            }

            ViewData["UserAvatar"] = userAvatar;

            return View(result.Value);
        }

        [HttpPost("/userpanel/ticket/add-message"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMessage(AddTicketMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = _localizer[message].ToString();
                return RedirectToAction("Details", new { id = model.TicketId });
            };

            model.SenderId = HttpContext.User.GetUserId();
            var result = await _ticketService.AddTicketMessageByUser(model);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction("Details", new { id = model.TicketId });
        }

        #endregion

        #region Create

        [HttpGet("/userpanel/ticket/add")]
        public IActionResult Create() => View(new CreateTicketViewModel());


        [HttpPost("/userpanel/ticket/add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.OwnerUserId = HttpContext.User.GetUserId();
            var result = await _ticketService.CreateByUserAsync(model);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("List");
            }
            else
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
        }

        #endregion

        #region Seen ticket

        [HttpPost("SeenTicketMessages/{id}")]
        public async Task<IActionResult> SeenTicketMessages(int id)
        {
            var result = await _ticketService.ChangeToSeenAllTicketMessages(id);

            return Ok(result);
        }

        #endregion

        #endregion
    }
}
