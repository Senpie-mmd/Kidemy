using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class FAQController : BaseController
    {
        #region Fields

        private readonly IFAQService _fAQService;
        private readonly IStringLocalizer _localizer;

        #endregion

        #region Ctor

        public FAQController(IFAQService fAQService,
            IStringLocalizer localizer)
        {
            _fAQService = fAQService;
            _localizer = localizer;
        }

        #endregion


        [HttpGet("/FAQs")]
        public async Task<IActionResult> ShowAllFAQs()
        {
            var result = await _fAQService.GetAllFAQs();

            if (result.IsFailure)
            {
                return NotFound();
            }

            return View(result.Value);
        }
    }
}
