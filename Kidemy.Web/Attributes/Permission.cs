using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kidemy.Web.Attributes
{
    public class Permission : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permissionName;

        public Permission(string permissionName)
        {
            _permissionName = permissionName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var roleService = context.HttpContext.RequestServices.GetRequiredService<IRoleService>();

            if (context.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                var userId = context.HttpContext.User.GetUserId();
                var userHasPermissionResult = await roleService.UserHasPermissionAsync(userId, _permissionName);

                if (userHasPermissionResult.IsFailure || !userHasPermissionResult.Value)
                {
                    context.Result = new RedirectResult("/?accessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/?accessDenied");
            }
        }
    }
}
