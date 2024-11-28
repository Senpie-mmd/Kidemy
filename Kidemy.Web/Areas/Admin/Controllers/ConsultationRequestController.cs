using Kidemy.Application.Convertors;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageConsultationRequest")]
    public class ConsultationRequestController : BaseAdminController
    {
        #region Fields
        private readonly IConsultationRequestService _consultationRequestService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor
        public ConsultationRequestController(IConsultationRequestService consultationRequestService, IStringLocalizer<SharedResource> localizer)
        {
            _consultationRequestService = consultationRequestService;
            _localizer = localizer;
        }
        #endregion

        #region Actions

        #region Filter
        [Permission("ConsultationRequestList")]
        [HttpGet]
        public async Task<IActionResult> Filter(AdminSideFilterConsultationRequestViewModel model)
        {
            var requests = await _consultationRequestService.AdminSideFilterAsync(model);

            return View(requests.Value);
        }
        #endregion

        #region Set time
        [Permission("SetTimeConsultationRequest")]
        [HttpGet]
        public async Task<IActionResult> SetTime(int id)
        {
            var request = await _consultationRequestService.AdminSideGetConsultaionRequestForSetTime(id);

            ViewBag.Request = request.Value;


            var model = new AdminSideSetTimeForConsultationRequestViewModel();
            model.ConsultationRequestId = request.Value.Id;
            if (request.Value.FixedTime != null)
            {
                model.SetFixedTime = request.Value.FixedTime.Value.ToLocalTime().TimeOfDay;
                model.SetFixedDate = request.Value.FixedTime.Value.ToUserDate();
            }

            return View(model);
        }
        [Permission("SetTimeConsultationRequest")]
        [HttpPost]
        public async Task<IActionResult> SetTime(AdminSideSetTimeForConsultationRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _consultationRequestService.SetTimeAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(Filter), new { area = "Admin" });
        }

        #endregion

        #region Cancele
        [Permission("ConsultationRequestCancele")]
        public async Task<ResponseResult> Cancele(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _consultationRequestService.CanceleRequestByAdmin(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #region Finished
        [Permission("ConsultationRequestFinish")]
        public async Task<ResponseResult> Finished(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _consultationRequestService.FinishedRequestByAdmin(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }


        #endregion

        #endregion
    }
}
