using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountUserVerficationService : IDiscountVerificationService
    {
        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a user exists within limitations
            var limitedUsers = discount.DiscountLimitations.SelectMany(dl => dl.Users).ToList();
            if (limitedUsers?.Any() ?? false)
            {
                return limitedUsers.Any(u => u.UserId == userId);
            }
            else
            {
                return true;
            }
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a user exists within limitations
            var limitedUsers = discount.DiscountLimitations.SelectMany(dl => dl.Users).ToList();
            if (limitedUsers?.Any() ?? false)
            {
                return limitedUsers.Any(u => u.UserId == userId);
            }
            else
            {
                return true;
            }
        }
    }
}
