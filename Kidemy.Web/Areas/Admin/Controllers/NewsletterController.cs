using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Newsletter;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    public class NewsletterController : BaseAdminController
    {
        #region Fields
        private readonly INewsletterService _newslettersService;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public NewsletterController(INewsletterService newslettersService,
            IStringLocalizer localizer)
        {
            _newslettersService = newslettersService;
            _localizer = localizer;
        }
        #endregion

        #region Actions
        [Permission("MembersList_Newsletter")]
        public async Task<IActionResult> MembersList(FilterNewsletterViewModel model)
        {
            model.TakeEntity = 20;
            var result = await _newslettersService.FilterAsync(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return View(model);
            }

            return View(result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission("DeleteMember_Newsletter")]
        public async Task<ResponseResult> DeleteMemberFromNewsletter(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
                return new ResponseResult(false);
            }

            var result = await _newslettersService.RemoveNewsletterMember(id);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return new ResponseResult(false);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return new ResponseResult(true);
        }
        #endregion  
    }
}
