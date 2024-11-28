using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class MasterContractViewComponent : ViewComponent
    {
        private readonly IUploadedMasterContractService _uploadedMasterContractService;

        public MasterContractViewComponent(IUploadedMasterContractService uploadedMasterContractService)
        {
            _uploadedMasterContractService = uploadedMasterContractService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int masterId)
        {

            var result = await _uploadedMasterContractService.GetMasterContractsPendingConfirmationListByMasterIdAsync(masterId);

            if (result.IsSuccess)
            {
                return View("MasterContract", result.Value);
            }

            else return null;

        }
    }
}
