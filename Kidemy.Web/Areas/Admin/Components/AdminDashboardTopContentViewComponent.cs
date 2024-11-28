using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Site;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Models.WithdrawRequest;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardTopContentViewComponent : ViewComponent
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly ITicketService _ticketService;
        private readonly ICourseRequestService _courseRequestService;
        private readonly IContactUsFormService _contactUsFormService;
        private readonly IWithdrawRequestService _withdrawRequestService;

        #endregion

        #region Constructor

        public AdminDashboardTopContentViewComponent(IUserService siteService, IContactUsFormService contactUsFormService
            , ITicketService ticketService
            , IWithdrawRequestService withdrawRequestService
            , IMasterService masterService
            , ICourseRequestService courseRequestService)
        {
            _userService = siteService;
            _contactUsFormService = contactUsFormService;
            _ticketService = ticketService;
            _withdrawRequestService = withdrawRequestService;
            _courseRequestService = courseRequestService;
            _masterService = masterService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AdminSideDashboardTopContentViewModel();

            var ticketCount = await _ticketService.GetTicketCount();
            model.TicketCount = ticketCount.Value;
            model.UserCount = await _userService.GetUserCount();
            model.ContactUsFormCount = await _contactUsFormService.GetContactUsFormCount();

            var todayUserCountResult = await _userService.GetTodayUserCountAsync();
            model.TodayUsersCount = todayUserCountResult.IsSuccess ? todayUserCountResult.Value : 0;

            var newWithdrawRequest = await _withdrawRequestService.GetWithdrawRequestCountAsync(WithdrawRequestStatus.Pending);
            model.NewWithdrawRequestCount = newWithdrawRequest.IsSuccess ? newWithdrawRequest.Value : 0;

            var newCourseRequest = await _courseRequestService.GetCountAsync(false);
            model.NewCourseRequestCount = newCourseRequest.IsSuccess ? newCourseRequest.Value : 0;
             
            var inProgressMasterRequestCount = await _masterService.GetCountAsync(MasterStatus.Pending);
            model.InProgressMasterRequestCount = inProgressMasterRequestCount.IsSuccess ? inProgressMasterRequestCount.Value : 0;

            return View("AdminDashboardTopContent", model);
        }

        #endregion

    }
}
