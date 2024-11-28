using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class HomePageBlogsViewComponent : ViewComponent
    {
        #region Fields

        private readonly IBlogService _blogService;

        #endregion  

        #region Ctor

        public HomePageBlogsViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _blogService.GetHomePageBlogs(8);

            if (result.IsFailure)
            {
                return View("HomePageBlogs", new List<ClientSideBlogViewModel>());
            }

            return View("HomePageBlogs", result.Value);
        }
    }
}
