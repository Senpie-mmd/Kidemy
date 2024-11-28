using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class AdviserController : BaseUserPanelController
    {
        #region Fields
        private readonly IAdviserSerivce _adviserSerivce;
        private readonly IConsultationRequestService _consultationRequestService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor
        public AdviserController(IAdviserSerivce adviserSerivce, IStringLocalizer<SharedResource> localizer, IConsultationRequestService consultationRequestService)
        {
            _adviserSerivce = adviserSerivce;
            _localizer = localizer;
            _consultationRequestService = consultationRequestService;
        }
        #endregion

        #region Actions

        #region Set Appointment

        [HttpGet("/set-appointment")]
        public async Task<IActionResult> SetAppointment(int adviserId)
        {
            var adviser = await _adviserSerivce.GetAdviserAsync(adviserId);

            ViewBag.Adviser = adviser.Value;

            return View();
        }

        [HttpPost("/set-appointment")]
        public async Task<IActionResult> SetAppointment(UpsertConsultationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var requestResult = await _consultationRequestService.CreateAsync(model);

                if (requestResult.IsFailure)
                {
                    TempData[ErrorMessage] = _localizer[requestResult.Message].ToString();
                    return View(model);
                }

                return RedirectToAction("GetConsultationRequest", "Adviser", new { area = "", consultationRequestId = requestResult.Value });

            }

            return View(model);
        }

        #endregion

        #region List

        [HttpGet]
        public async Task<IActionResult> List(ClientSideFilterConsultationRequestViewModel model)
        {
            var appointments = await _consultationRequestService.ClientSideFilterAsync(model);
            return View(appointments.Value);
        }


        #endregion

        #region Cancele
        public async Task<ResponseResult> Cancele(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _consultationRequestService.CanceleRequestByUser(id);

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
