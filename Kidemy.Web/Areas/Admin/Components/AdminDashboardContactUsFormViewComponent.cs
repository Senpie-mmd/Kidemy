using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardContactUsFormViewComponent : ViewComponent
    {
        #region Fields

        private readonly IContactUsFormService _contactUsFormService;

        #endregion

        #region Ctor

        public AdminDashboardContactUsFormViewComponent(IContactUsFormService contactUsFormService)
        {
            _contactUsFormService = contactUsFormService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _contactUsFormService.FilterContactUsForm(new Application.ViewModels.ContactUs.FilterContactUsFormViewModel
            {
                TakeEntity = 5
            });

            return View("AdminDashboardContactUsForm", result.Value);
        }

    }
}
