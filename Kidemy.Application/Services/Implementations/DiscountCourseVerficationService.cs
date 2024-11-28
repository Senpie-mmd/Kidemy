using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountCourseVerficationService : IDiscountVerificationService
    {
        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a course exists within limitations
            var limitedCourses = discount.DiscountLimitations.SelectMany(dl => dl.Courses).ToList();
            if (limitedCourses?.Any() ?? false)
            {
                return limitedCourses.Any(c => c.CourseId == course.Id);
            }
            else
            {
                return true;
            }
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a course exists within limitations
            var limitedCourses = discount.DiscountLimitations.SelectMany(dl => dl.Courses).ToList();
            if (limitedCourses?.Any() ?? false)
            {
                return limitedCourses.Any(p => cart?.Items?.Select(item => item.CourseId).Contains(p.CourseId) ?? false);
            }
            else
            {
                return true;
            }
        }
    }
}
