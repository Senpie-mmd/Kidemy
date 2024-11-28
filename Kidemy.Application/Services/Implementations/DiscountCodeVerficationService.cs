using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountCodeVerficationService : IDiscountVerificationService
    {
        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            if (discount.AutoUse)
            {
                return true;
            }
            else
            {
                return discount.Code == enteredDiscountCode;
            }
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            if (discount.AutoUse)
            {
                return true;
            }
            else
            {
                return discount.Code == enteredDiscountCode;
            }
        }
    }
}
