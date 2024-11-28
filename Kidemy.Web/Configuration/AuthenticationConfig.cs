using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kidemy.Web.Configuration
{
    public static class AuthenticationConfig
    {
        public static void ConfigAuthentication(this WebApplicationBuilder builder)
        {
            #region Authentication

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/login";
                option.LogoutPath = "/logout";
                option.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            #endregion
        }
    }
}
