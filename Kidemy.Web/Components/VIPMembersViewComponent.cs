using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class VIPMembersViewComponent : ViewComponent
    {
        #region Fields

        private readonly IVIPMembersService _vIPMembersService;
        private readonly IDynamicTextService _dynamicTextService;

        #endregion

        #region Ctor

        public VIPMembersViewComponent(IVIPMembersService vIPMembersService, IDynamicTextService dynamicTextService)
        {
            _vIPMembersService = vIPMembersService;
            _dynamicTextService = dynamicTextService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var vIPDashBoardTextBeforeJoin = await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.VIPDashBoardTextBeforeJoin);
            var vIPDashBoardTextAfterJoin = await _dynamicTextService.GetDynamicTextByPosition(Domain.Enums.DynamicText.DynamicTextPosition.VIPDashBoardTextAfterJoin);


            var vIPUserInformation = await _vIPMembersService.GetVIPUserInformation(userId);

            if (!vIPUserInformation.Value.IsVIPUser)
            {
                ViewData["VIPDashBoardText"] = vIPDashBoardTextBeforeJoin.Value.Text;
            }
            else
            {
                ViewData["VIPDashBoardText"] = vIPDashBoardTextAfterJoin.Value.Text;

            }
            return View("VIPMembers", vIPUserInformation.Value);
        }
    }
}
