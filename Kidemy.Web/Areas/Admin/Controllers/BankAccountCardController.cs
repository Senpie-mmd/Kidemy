using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Domain.Enums.BankAccount;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageAccountBankCard")]
    public class BankAccountCardController : BaseAdminController
    {
        #region Fields

        private readonly IBankAccountCardService _bankAccountCardService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public BankAccountCardController(IStringLocalizer<SharedResource> localizer, IBankAccountCardService bankAccountCardService)
        {
            _localizer = localizer;
            _bankAccountCardService = bankAccountCardService;
        }

        #endregion

        #region Actions

        #region List

        [Permission("BankAccountCardList")]
        public async Task<IActionResult> BankAccountCardList(FilterBankAccountCardViewModel filter)
        {
            var result = await _bankAccountCardService.FilterBankAccountCardAsync(filter);

            return View(result.Value);
        }

        #endregion

        #region ChangeStatus

       [Permission("ConfirmBankAccountCard")]
        public async Task<IActionResult> ConfirmBankAccountCard(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(BankAccountCardList), new { area = "Admin" });
            }

            var result = await _bankAccountCardService.ChangeStatusAsync(id);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            else
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();

            return RedirectToAction(nameof(BankAccountCardList), new { area = "Admin" });
        }

        [Permission("RejectBankAccountCard")]
        public async Task<IActionResult> RejectBankAccountCard(ChangeStatusBankAccountCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(BankAccountCardList), new { area = "Admin" });
            }

            var result = await _bankAccountCardService.ChangeStatusAsync(model);

            if (result.IsFailure)
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            else
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();

            return RedirectToAction(nameof(BankAccountCardList), new { area = "Admin" });
        }

        #endregion

        #region ShowRejectModal

        public async Task<IActionResult> ShowRejectModal(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.NullValue].ToString();
                return RedirectToAction(nameof(BankAccountCardList), new { area = "Admin" });
            }

            var model = new ChangeStatusBankAccountCardViewModel
            {
                Id = id,
                Status = BankAccountCardStatus.Rejected
            };

            return PartialView("_RejectBankAccountCard", model);
        }

        #endregion

        #endregion
    }
}
