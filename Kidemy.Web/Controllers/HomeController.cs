using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kidemy.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICourseService _courseService;

        #endregion

        #region Constructor

        public HomeController(IStringLocalizer<SharedResource> localizer, ICourseService courseService)
        {
            _localizer = localizer;
            _courseService = courseService;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var favouriteCourses = await _courseService.GetAllUserFavouriteCoursesAsync(User.GetUserId());
            if (favouriteCourses.IsSuccess && favouriteCourses.Value.Any())
                ViewBag.FavouriteCourses = favouriteCourses.Value;

            return View();
        }


        #region Save Ckeditor Image

        [HttpPost("SaveCkeditorImage")]
        public async Task<JsonResult> SaveCkeditorImage(IFormFile upload)
        {
            if (upload.Length <= 0 || !upload.IsImage()) { return null; }

            string fileName = CommonTools.GenerateUniqueStringCode() +
                              Path.GetExtension(upload.FileName).ToLower();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ckeditor");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            string url = $"{"/images/ckeditor/"}{fileName}";

            return Json(new { uploaded = true, url });
        }

        #endregion

        #region Delete Ckeditor Image

        [HttpPost("DeleteCkeditorImage")]
        public JsonResult DeleteCkeditorImage(List<string> imagesName)
        {
            if (imagesName.Any())
            {
                foreach (string name in imagesName)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory() + $"/wwwroot{name}");

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            return Json("");
        }

        #endregion

        #region Get Resource

        public string GetResource(string resourceName)
        {
            return _localizer.GetString(resourceName);
        }

        #endregion

        #region Change Language

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ChangeLanguage(string id)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(id)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrWhiteSpace(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
