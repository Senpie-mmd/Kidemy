using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class AboutUsProgressBarViewComponent : ViewComponent
    {
        #region Fields
        private readonly IAboutUsService _aboutUsService;
        #endregion

        #region Ctor
        public AboutUsProgressBarViewComponent(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _aboutUsService.GetAllProgressBarList();
            if (result.IsFailure)
            {
                List<ProgressBarListViewModel> model = new List<ProgressBarListViewModel>();
                return View("ProgressBar", model);
            }
            return View("ProgressBar", result.Value);
        }
    }
}
