using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Blog;
using Kidemy.Application.ViewModels.Blog.ClientSideBlog.Comments;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class BlogController : BaseController
    {
        #region Fields
        private readonly IBlogService _blogService;
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Ctor
        public BlogController(IBlogService blogService,
            IStringLocalizer localizer)
        {
            _blogService = blogService;
            _localizer = localizer;
        }
        #endregion

        #region Blog

        [HttpGet("/blogs")]
        public async Task<IActionResult> BlogsList(ClientSideFilterBlogViewModel filter)
        {
            filter.TakeEntity = 8;
            var result = await _blogService.FilterAsync(filter);

            if (result.IsFailure)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        [HttpGet("blog/{slug}")]
        public async Task<IActionResult> Blog(string slug)
        {
            if (slug is null) return NotFound();
            var result = await _blogService.GetBlogDetailBySlug(slug);
            if (result.IsFailure)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        #endregion

        #region Comment

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> CreateComments(ClientSideCreateBlogCommentViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return ResponseResult.Failure(_localizer[ErrorMessages.UserNotLogin].ToString());
            }

            if (!ModelState.IsValid)
            {
                //get first error text
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;

                return ResponseResult.Failure(_localizer[firstError].ToString());
            }
            var result = await _blogService.CreateComment(model);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }
            else
            {
                return ResponseResult.Success(_localizer[result.Message].ToString());
            }
        }

        #endregion
    }
}
