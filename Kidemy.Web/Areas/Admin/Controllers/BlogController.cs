using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Blog;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs;
using Kidemy.Domain.Enums.Blog;
using Kidemy.Domain.Shared;
using Kidemy.Web.Attributes;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("ManageBlogs")]
    public class BlogController : BaseAdminController
    {
        #region Fields

        private readonly IBlogService _blogService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IBlogCommentService _blogCommentService;

        #endregion

        #region Constructor

        public BlogController(IBlogService BlogService, IStringLocalizer<SharedResource> localizer, IBlogCommentService blogCommentService)
        {
            _blogService = BlogService;
            _localizer = localizer;
            _blogCommentService = blogCommentService;
        }
        #endregion

        #region Actions 

        [Permission("BlogsList")]
        public async Task<IActionResult> List(FilterBlogViewModel filter)
        {
            var result = await _blogService.FilterAsync(filter);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("CreateBlog")]
        public IActionResult Create()
        {
            return View(new AdminSideUpsertBlogViewModel());
        }

        [Permission("CreateBlog")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideUpsertBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _blogService.CreateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });

        }

        [Permission("UpdateBlog")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _blogService.GetBlogForEditByIdAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(List), new { area = "Admin" });
            }

            return View(result.Value);

        }

        [Permission("UpdateBlog")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminSideUpsertBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _blogService.UpdateAsync(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(List), new { area = "Admin" });

        }

        [Permission("DeleteBlog")]
        public async Task<ResponseResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return ResponseResult.Failure(ErrorMessages.FailedOperationError);
            }

            var result = await _blogService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return ResponseResult.Success(SuccessMessages.SuccessfullyDone);
        }

        #region Comments 

        public async Task<IActionResult> LoadBlogComments(AdminSideFilterBlogCommentsViewModel filter)
        {
            if (filter.BlogId < 1) return PartialView("_BlogComments");

            var result = await _blogCommentService.FilterCommentsAdminSide(filter);
            if (result.IsFailure)
            {
                return PartialView("_BlogComments", new AdminSideFilterBlogCommentsViewModel());
            }

            return PartialView("_BlogComments", result.Value);
        }

        public async Task<IActionResult> ConformBlogComment(int commentId,
            int currentPage = 1,
            bool? IsConfirmed = null,
            string? CommentMessage = null,
            int? UserId = null,
            string? UserName = null)
        {
            if (commentId < 1) return null;

            var result = await _blogCommentService.ConfirmOrDenyComment(commentId, true);
            if (result.IsFailure)
                return null;

            return RedirectToAction(nameof(LoadBlogComments),
                new
                {
                    BlogId = result.Value,
                    Page = currentPage,
                    IsConfirmed = IsConfirmed,
                    CommentMessage = CommentMessage,
                    UserId = UserId,
                    UserName = UserName
                });
        }

        public async Task<IActionResult> DenyBlogComment(int commentId,
            int currentPage = 1,
            bool? IsConfirmed = null,
            string? CommentMessage = null,
            int? UserId = null,
            string? UserName = null)
        {
            if (commentId < 1) return null;

            var result = await _blogCommentService.ConfirmOrDenyComment(commentId, false);
            if (result.IsFailure)
                return null;

            return RedirectToAction(nameof(LoadBlogComments),
                new
                {
                    BlogId = result.Value,
                    Page = currentPage,
                    IsConfirmed = IsConfirmed,
                    CommentMessage = CommentMessage,
                    UserId = UserId,
                    UserName = UserName
                });
        }

        #endregion


        #endregion
    }
}
