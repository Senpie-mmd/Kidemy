using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class BankAccountCardController : BaseUserPanelController
    {
        #region Fields

        private readonly IBankAccountCardService _bankAccountCardService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public BankAccountCardController(IBankAccountCardService bankAccountCardService, IStringLocalizer<SharedResource> localizer)
        {
            _bankAccountCardService = bankAccountCardService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region List

        [HttpGet("/userpanel/bank-account-cards")]
        public async Task<IActionResult> List(FilterBankAccountCardViewModel model)
        {
            model.UserId = User.GetUserId();
            var result = await _bankAccountCardService.FilterBankAccountCardAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet("/userpanel/add-bank-account-card")]
        public IActionResult Create()
        {
            return View(new UpsertBankAccountCardViewModel());
        }

        [HttpPost("/userpanel/add-bank-account-card")]
        public async Task<IActionResult> Create(UpsertBankAccountCardViewModel model)
        {

            model.UserId = User.GetUserId();
            if (!ModelState.IsValid) return View();

            var result = await _bankAccountCardService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region SetDefault

        [HttpGet("/userpanel/setdefaultbankaccountcard/{id:int}")]
        public async Task<IActionResult> SetDefault(int id)
        {
            var result = await _bankAccountCardService.SetDefultBankAccountCardAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Delete

        [HttpGet("/userpanel/deletebankaccountcard/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bankAccountCardService.DeleteBankAccountCardAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }

            return RedirectToAction(nameof(List));
        }

        #endregion

        #endregion

    }
}
