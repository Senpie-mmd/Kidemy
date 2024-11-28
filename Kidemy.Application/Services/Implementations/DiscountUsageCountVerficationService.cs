using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Enums.Discount;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountUsageCountVerficationService : IDiscountVerificationService
    {
        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a usage count exists within limitations
            var limitedUsageCount = discount.DiscountLimitations
                .FirstOrDefault(dl => dl.Type == DiscountLimitationType.UsageCount)
                ?.UsageCount;

            if (limitedUsageCount is not null)
            {
                return discount.DiscountUsages?.Count(item => item.IsFinally) < limitedUsageCount.Count;
            }
            else
            {
                return true;
            }
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a usage count exists within limitations
            var limitedUsageCount = discount.DiscountLimitations
                .FirstOrDefault(dl => dl.Type == DiscountLimitationType.UsageCount)
                ?.UsageCount;

            if (limitedUsageCount is not null)
            {
                return discount.DiscountUsages?.Count(item => item.IsFinally) < limitedUsageCount.Count;
            }
            else
            {
                return true;
            }
        }
    }
}
