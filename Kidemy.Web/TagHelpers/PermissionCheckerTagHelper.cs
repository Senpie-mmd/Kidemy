using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Kidemy.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "permission")]
    public class PermissionCheckerTagHelper : TagHelper
    {
        #region Fields
        private readonly IRoleService _roleService;
        #endregion

        #region Ctor
        public PermissionCheckerTagHelper(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }
        [HtmlAttributeName("permission")]
        public string PermissionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (!ViewContextData.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            var userId = ViewContextData.HttpContext.User.GetUserId();
            var permissions = PermissionName
                .Split("||")
                .Select(n => n.Trim())
                .ToList();

            if (permissions == null || !permissions.Any())
                return;

            if (permissions.Any(permission => _roleService.BoolUserHasPermissionAsync(userId, permission).Result))
                return;

            output.SuppressOutput();
        }
    }
}
