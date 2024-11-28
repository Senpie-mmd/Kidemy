using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IDiscountVerificationService
    {
        bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null);

        bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null);
    }
}
