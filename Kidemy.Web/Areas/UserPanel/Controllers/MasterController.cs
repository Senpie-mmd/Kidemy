using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class MasterController : BaseUserPanelController
    {
        #region Fields

        private readonly IMasterService _masterService;
        private readonly IMasterContractService _masterContractService;
        private readonly IMasterNotificationService _masterNotificationService;
        private readonly IUploadedMasterContractService _uploadedMasterContractService;
        private readonly IDynamicTextService _dynamicTextService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public MasterController(IStringLocalizer<SharedResource> localizer, IDynamicTextService dynamicTextService, IMasterService masterService, IMasterContractService masterContractService, IUploadedMasterContractService uploadedMasterContractService, IMasterNotificationService masterNotificationService)
        {
            _localizer = localizer;
            _dynamicTextService = dynamicTextService;
            _masterService = masterService;
            _masterContractService = masterContractService;
            _uploadedMasterContractService = uploadedMasterContractService;
            _masterNotificationService = masterNotificationService;

        }

        #endregion

        #region Actions

        #region AddMaster

        [HttpGet("userpanel/add-master")]
        public async Task<IActionResult> AddMaster()
        {
            return View();
        }

        [HttpPost("userpanel/add-master"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMaster(ClientSideRegisterMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = HttpContext.User.GetUserId();
            Result result = await _masterService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(EditMaster));
        }

        #endregion

        #region EditMaster

        [HttpGet("userpanel/edit-master")]
        public async Task<IActionResult> EditMaster()
        {
            var result = await _masterService.GetMasterForEditByIdAsync(User.GetUserId());

            if (result.IsFailure)
            {
                return RedirectToAction(nameof(AddMaster));
            }

            return View(result.Value);
        }

        [HttpPost("userpanel/edit-master"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMaster(ClientSideUpdateMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = HttpContext.User.GetUserId();
            Result result = await _masterService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return View(model);
        }

        #endregion

        #region MasterContractsList

        [HttpGet("userpanel/master-contracts")]
        public async Task<IActionResult> MasterContractsList()
        {
            var masterId = HttpContext.User.GetUserId();

            var result = await _masterContractService.GetMasterContractsAsync();

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View();
            }

            var uploadedMasterContractsResult = await _masterContractService.GetUploadedMasterContractsAsync(masterId);
            if (uploadedMasterContractsResult.IsSuccess)
            {
                ViewData["UploadedMasterContracts"] = uploadedMasterContractsResult.Value;
            }

            return View(result.Value);
        }

        #endregion

        #region DownloadMasterContract
        public IActionResult DownloadMasterContract(string fileMasterContractName)
        {

            string filePath = Directory.GetCurrentDirectory() + "/wwwroot" + SiteTools.MasterContractFilePath + fileMasterContractName;

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

        #region UploadedMasterContract

        [HttpPost]
        public async Task<IActionResult> UploadedMasterContract(ClientSideCreateUploadedMasterContractViewModel model)
        {
            model.MasterId = HttpContext.User.GetUserId();
            Result result;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isExistUploaded = _uploadedMasterContractService.ExistUploadedMasterContractByIdAsync(model.MasterContractId, model.MasterId).Result.Value;

            if (isExistUploaded == true)
            {
                int id = _uploadedMasterContractService.GetUploadedMasterContractIdByMasterContractIdAsync(model.MasterContractId).Result.Value;

                ClientSideUpdateUploadedMasterContractViewModel clientSideUpdateUploadedMasterContractViewModel = new ClientSideUpdateUploadedMasterContractViewModel()
                {
                    Id = id,
                    File = model.File,
                    FileName = model.FileName,
                    MasterContractId = model.MasterContractId,
                    MasterId = model.MasterId,
                    Status = model.Status
                };

                result = await _uploadedMasterContractService.UpdateAsync(clientSideUpdateUploadedMasterContractViewModel);
            }
            else
            {
                result = await _uploadedMasterContractService.CreateAsync(model);
            }

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(MasterContractsList));
        }

        #endregion

        #region MasterContractsList

        [HttpGet("userpanel/master-notifications")]
        public async Task<IActionResult> MasterNotificationsList()
        {
            var masterId = HttpContext.User.GetUserId();

            var result = await _masterNotificationService.GetMasterNotificationsAsync(masterId);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View();
            }

            return View(result.Value);
        }

        #endregion

        #endregion
    }
}



