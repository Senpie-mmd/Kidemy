using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.MasterContract;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageMastersContracts")] 
    public class MasterContractController : BaseAdminController
    {
        #region Fields

        private readonly IMasterContractService _masterContractService;
        private readonly IUploadedMasterContractService _uploadedMasterContractService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public MasterContractController(IMasterContractService masterContractService, IStringLocalizer<SharedResource> localizer, IUploadedMasterContractService uploadedMasterContractService)
        {
            _masterContractService = masterContractService;
            _localizer = localizer;
            _uploadedMasterContractService = uploadedMasterContractService;
        }

        #endregion

        #region Actions

        #region List

        [Permission("MasterContractsList")] 
        public async Task<IActionResult> List(FilterMasterContractViewModel filter)
        {
            var result = await _masterContractService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }

            return View(result.Value);

        }

        #endregion

        #region Create

        [Permission("CreateMasterContract")]
        public IActionResult Create()
        {
            return View(new AdminSideCreateMasterContractViewModel());

        }

        [Permission("CreateMasterContract")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideCreateMasterContractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _masterContractService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));

        }

        #endregion

        #region Update

        [Permission("UpdateMasterContract")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _masterContractService.GetMasterContractForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateMasterContract")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpdateMasterContractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _masterContractService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(List));

        }

        #endregion

        #region Delete

        [Permission("DeleteMasterContract")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return new ResponseResult(false);
            }

            var result = await _masterContractService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #region List

        [Permission("PendingContractsList")]
        public async Task<IActionResult> PendingContractsList(FilterMasterInformationForContractsPendingConfirmationViewModel filter)
        {
            var result = await _uploadedMasterContractService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(result.Value);

        }

        #endregion

        #region MasterContractsDetails

        [Permission("MasterContractsDetails")]
        public async Task<IActionResult> MasterContractsDetails(int masterId)
        {
            var result = await _uploadedMasterContractService.GetMasterContractsPendingConfirmationListByMasterIdAsync(masterId);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(PendingContractsList), new { area = "Admin" });
            }

            return PartialView("_MasterContractsDetails",result.Value);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMasterContractsPendingConfirmation(List<AdminSideUpdateUploadedMasterContractViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _uploadedMasterContractService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }


            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(PendingContractsList));

        }
        #endregion

        #region DownloadMasterContract
        public IActionResult DownloadMasterContract(string fileMasterContractName)
        {

            string filePath = Directory.GetCurrentDirectory() + "/wwwroot" + SiteTools.UploadedMasterContractFilePath + fileMasterContractName;

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                string fileName = fileMasterContractName;
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileName,
                    Inline = false,
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {

                return NotFound();
            }
        }
        #endregion

        #endregion
    }
}
