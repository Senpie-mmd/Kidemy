using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Permission("AdminPanel")]
    public class HomeController : BaseAdminController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Constructor

        public HomeController(IUserService userService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Actions

        #region User
        public async Task<IActionResult> SearchUserModal(FilterUsersViewModel filter, string baseName)
        {
            filter.TakeEntity = 5;
            ViewBag.baseName = baseName;

            var model = await _userService.FilterUsersAsync(filter);

            return PartialView("_FilterUserPartialView", model.Value);
        }

        #endregion

        #region UserForSendEmail

        public async Task<IActionResult> SearchUserModalForSendEmail(FilterUsersViewModel filter)
        {
            filter.IsEmailExist = true;
            var result = await _userService.FilterUsersAsync(filter);


            if (result.IsFailure)
            {
                return BadRequest("");
            }

            return PartialView("_FilterUserForSendEmailPartialView", result.Value);
        }

        #endregion

        #region UserSendSMS

        public async Task<IActionResult> SearchUserModalForSendSMS(FilterUsersViewModel filter, string baseName)
        {
            ViewBag.baseName = baseName;
            filter.IsMobileExist = true;
            filter.IsMobileActive = true;
            var result = await _userService.FilterUsersAsync(filter);

            if (result.IsFailure)
            {
                return BadRequest("");
            }

            return PartialView("_FilterUserForSendSMSPartialView", result.Value);
        }

        #endregion

        #region Download

        public async Task<ActionResult> DownloadAsZip(List<string> paths)
        {
            if (paths is not null && paths.Any())
            {
                var memoryStream = new MemoryStream();

                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var path in paths)
                    {
                        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(string.Concat(_hostingEnvironment.WebRootPath, path));
                        string fileName = Path.GetFileName(path);

                        var zipEntry = archive.CreateEntry(fileName, CompressionLevel.Fastest);
                        using (var zipStream = zipEntry.Open())
                        {
                            zipStream.Write(fileBytes, 0, fileBytes.Length);
                        }
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/zip", "images.zip");
            }

            return BadRequest();
        }

        #endregion  

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
