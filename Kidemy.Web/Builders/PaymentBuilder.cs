using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Enums.Payment;
using Kidemy.Domain.Shared;
using Kidemy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Builders
{
    public static class PaymentBuilder
    {
        public static async Task<Result<PaymentController>> BuildActivePaymentController(HttpContext httpContext)
        {
            var siteSettingService = httpContext.RequestServices.GetRequiredService<ISiteSettingService>();

            var siteSettingResult = await siteSettingService.GetSiteSettingAsync();

            if (siteSettingResult.IsFailure)
            {
                return Result.Failure<PaymentController>(siteSettingResult.Message);
            }

            var siteSetting = siteSettingResult.Value;

            PaymentController? controller;

            controller = siteSetting.DefaultPaymentMethod switch
            {
                PaymentMethodType.ZarinPal => httpContext.RequestServices.GetRequiredService<ZarinPalPaymentController>(),
                _ => null
            };

            if (controller is null) Result.Failure<PaymentController>(ErrorMessages.ImageIsNotValidError);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            return controller;
        }
    }
}
