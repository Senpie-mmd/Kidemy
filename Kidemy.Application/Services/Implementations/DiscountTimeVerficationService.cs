using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Models.User;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Application.ViewModels.Cart;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountTimeVerficationService : IDiscountVerificationService
    {
        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            var isValid = true;

            if (discount.StartDateTimeOnUtc.HasValue && discount.StartDateTimeOnUtc > DateTime.UtcNow)
            {
                isValid = false;
            }

            if (discount.EndDateTimeOnUtc.HasValue && discount.EndDateTimeOnUtc < DateTime.UtcNow)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            var isValid = true;

            if (discount.StartDateTimeOnUtc.HasValue && discount.StartDateTimeOnUtc > DateTime.UtcNow)
            {
                isValid = false;
            }

            if (discount.EndDateTimeOnUtc.HasValue && discount.EndDateTimeOnUtc < DateTime.UtcNow)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
