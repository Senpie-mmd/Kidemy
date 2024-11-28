using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Shared;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace Kidemy.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAccountCodeService _accountService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IEncryptService _encryptService;

        #endregion

        #region Constructor

        public AccountController(
            IAccountCodeService accountService,
            IRoleService roleService,
            IUserService userService,
            IStringLocalizer<SharedResource> localizer,
            IEncryptService encryptService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _userService = userService;
            _localizer = localizer;
            _encryptService = encryptService;
        }

        #endregion

        #region Actions

        #region Login

        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = "")
        {
            TempData["ReturnUrl"] = returnUrl;

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            if (!ModelState.IsValid) return View(model);

            var result = await _userService.ConfirmLogin(model);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            if (!result.Value.IsMobileActive)
            {
                TempData["Mobile"] = _encryptService.EncryptText(result.Value.Mobile);
                return RedirectToAction(nameof(ConfirmMobile));
            }

            await LoginUser(result.Value, model.RememberMe);

            var returnUrl = TempData["ReturnUrl"].ToString();

            var nextUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? returnUrl
                : "/";

            return Redirect(nextUrl);
        }


        #endregion

        #region Register

        [HttpGet("/register")]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false) return Redirect("/");

            return View();
        }


        [HttpPost("/register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (User.Identity?.IsAuthenticated ?? false) return Redirect("/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.AcceptTerms)
            {
                ModelState.AddModelError(nameof(model.AcceptTerms), _localizer[ErrorMessages.AcceptTermsError].ToString());
                return View(model);
            }

            var registerResult = await _userService.RegisterUserAsync(model);

            if (registerResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[registerResult.Message].ToString();
                return View(model);
            }

            TempData["Mobile"] = _encryptService.EncryptText(model.Mobile);
            return RedirectToAction(nameof(ConfirmMobile));
        }


        #endregion

        #region Confirm mobile

        [HttpGet("confirm-mobile")]
        public ActionResult ConfirmMobile()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost("confirm-mobile"), ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmMobile(ConfirmMobileViewModel model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            model.Mobile = _encryptService.DecryptText(TempData["Mobile"]?.ToString() ?? "");

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid) return View(model);

            var result = await _userService.ConfirmUserMobileAsync(model);

            if (result.IsFailure)
            {
                TempData["Mobile"] = _encryptService.EncryptText(model.Mobile);
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            var userResult = await _userService.GetUserByMobileAsync(model.Mobile);

            if (userResult.IsFailure)
            {
                TempData["Mobile"] = _encryptService.EncryptText(model.Mobile);
                TempData[ErrorMessage] = _localizer[userResult.Message].ToString();
                return View(model);
            }

            var returnUrl = TempData["ReturnUrl"].ToString();

            if (!returnUrl.StartsWith("/reset-password"))
            {
                await LoginUser(userResult.Value);
            }

            var nextUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? returnUrl
                : "/";

            return Redirect(nextUrl);
        }

        #endregion

        #region Forgout Password

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost("forgot-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string mobile)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            var existsUserResult = await _userService.ExistsUser(mobile);

            if (existsUserResult.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.UserNotFoundError].ToString();
                return View();
            }

            if(existsUserResult.IsSuccess && existsUserResult.Value == false)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.UserNotFoundError].ToString();
                return View();
            }

            var result = await _userService.SendMobileConfirmationCodeAsync(mobile);

            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View();
            }

            TempData["Mobile"] = _encryptService.EncryptText(mobile);
            TempData["ReturnUrl"] = "/reset-password?mobile=" + _encryptService.EncryptText(mobile);

            return RedirectToAction(nameof(ConfirmMobile));
        }


        #endregion

        #region Reset Password

        [HttpGet("reset-password")]
        public ActionResult ResetPassword(string mobile)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            TempData["Mobile"] = mobile.Replace(" ", "+");
            return View();
        }


        [HttpPost("reset-password"), ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mobile = _encryptService.DecryptText(TempData["Mobile"]?.ToString() ?? "");
            var password = model.Password;

            var result = await _userService.ResetPassword(mobile, password);

            if (result.IsFailure)
            {
                TempData["Mobile"] = _encryptService.EncryptText(mobile);
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(model);
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();
            return RedirectToAction(nameof(Login));
        }


        #endregion

        #region Activate Email

        [HttpGet("activate-email/{confirmationCode}")]
        public async Task<IActionResult> ActivateEmail(string confirmationCode)
        {
            var result = await _userService.ConfirmUserEmailAsync(confirmationCode);

            if (result.IsFailure)
            {
                return NotFound();
            }
            else
            {
                return View("EmailActived");
            }
        }

        #endregion

        #region Logout

        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #endregion

        #region Utilities

        private async Task LoginUser(ClientSideUserViewModel user, bool rememberMe = false)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.MobilePhone, user.Mobile)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
            };

            await HttpContext.SignInAsync(principal, properties);

            // migrate cart
            // await _cookieBaseCartService.MigrateToRealCart(user.Id);
        }

        #endregion
    }
}