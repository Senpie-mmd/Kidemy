using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class MasterController : BaseController
    {
        #region Fields

        private readonly IMasterService _masterService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        #endregion

        #region Constructor

        public MasterController(
            IMasterService masterService,
            IStringLocalizer<SharedResource> localizer)
        {
            _masterService = masterService;
            _localizer = localizer;
        }

        #endregion

        #region Actions

        #region MasterList

        [HttpGet("/master-list")]
        public async Task<IActionResult> MasterList(FilterForClientSideMasterViewModel filter)
        {

            var result = await _masterService.FilterForClientSideAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(result.Value);
        }

        #endregion

        #region MasterDetails

        [HttpGet("/master-details")]
        public async Task<IActionResult> MasterDetails(int id)
        {

            var result = await _masterService.GetMasterDetailsByIdAsync(id);

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
