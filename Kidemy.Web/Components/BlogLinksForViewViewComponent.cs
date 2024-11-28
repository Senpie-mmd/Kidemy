using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Enums.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
    public class BlogLinksForViewViewComponent : ViewComponent
    {
        #region Fields
        private readonly IBlogService _blogService;
        private ILogger<BlogLinksForViewViewComponent> _logger;
        #endregion

        #region Ctor
        public BlogLinksForViewViewComponent(IBlogService blogService,
            ILogger<BlogLinksForViewViewComponent> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync(LinkPlace place)
        {
            return View();
            //var result = await _blogService.GetBlogLinks(place);
            //if (result.IsFailure)
            //{
            //    _logger.LogError("Get blog links returned isFail true.");
            //}

            //if (place == LinkPlace.Footer)
            //{
            //    return View("BlogLinksForFooter", result.Value);
            //}
            //else
            //{
            //    return View("BlogLinksForHeader", result.Value);
            //}
        }
    }
}
