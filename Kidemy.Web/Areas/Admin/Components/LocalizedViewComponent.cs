using Barnamenevisan.Localizing.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    [ViewComponent]
    public class LocalizedViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(object model)
        {
            if (model != null)
            {
                ViewBag.LocalizableModel = model;

                var preparedLocalizedModelsForView =
                            (List<LocalizedViewModel>)
                            model?
                            .GetType()
                            .GetProperty(nameof(LocalizableViewModel<LocalizedViewModel>.PreparedLocalizedModelsForView))
                            ?.GetValue(model);

                return View("Localized", preparedLocalizedModelsForView);
            }

            return View("Localized");
        }
    }
}
