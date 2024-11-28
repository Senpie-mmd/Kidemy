using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountNotUsedByUserVerficationService : IDiscountVerificationService
    {
        private readonly IDiscountService _discountService;

        public DiscountNotUsedByUserVerficationService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode)
        {
            if (userId == 0) return true;

            var isUsedResult = _discountService.DiscountIsUsedByUser(discount.Id, userId).Result;

            return isUsedResult.IsSuccess && isUsedResult.Value == false;
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode)
        {
            if (userId == 0) return true;

            var isUsedResult = _discountService.DiscountIsUsedByUser(discount.Id, userId).Result;

            return isUsedResult.IsSuccess && isUsedResult.Value == false;
        }
    }
}
