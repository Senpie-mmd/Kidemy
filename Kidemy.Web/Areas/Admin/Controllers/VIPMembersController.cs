using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    public class VIPMembersController : BaseAdminController
    {
        #region Fields

        private readonly IVIPPlanService _vIPPlanService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public VIPMembersController(IVIPPlanService vIPPlanService, IStringLocalizer<SharedResource> localizer)
        {
            _vIPPlanService = vIPPlanService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region AssignPlanForUserByAdmin

        [HttpPost]
        public async Task<IActionResult> AssignPlanForUserByAdmin(int PlanId, int UserId)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values
                        .First(v => v.ValidationState is ModelValidationState.Invalid)
                        .Errors
                        .First()
                        .ErrorMessage;

                TempData[ErrorMessage] = _localizer[message];
                return RedirectToAction("Profile", "User", new { id = UserId });
            }

            var result = await _vIPPlanService.AssignPlanForUserByAdminAsync(PlanId, UserId);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Profile", "User", new { id = UserId });
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyUserJoinVIPMember].ToString();
            }

            return RedirectToAction("Profile", "User", new { id = UserId });

        }

        #endregion 

        #endregion

    }
}
