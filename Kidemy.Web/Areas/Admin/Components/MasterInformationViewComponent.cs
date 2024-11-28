using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class MasterInformationViewComponent : ViewComponent
    {
        private readonly IMasterService _masterService;

        public MasterInformationViewComponent(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var result = await _masterService.GetMasterInfoAsync(id);

            if (result.IsSuccess)
            {
                return View("MasterInformation", result.Value);
            }

            else return null;

        }
    }
}
