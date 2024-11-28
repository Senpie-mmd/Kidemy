using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Domain.Models.Cart;

namespace Kidemy.Application.Mapper
{
    public static class CartMapper
    {
        public static CartViewModel MapFrom(this CartViewModel model, Cart cart, List<ClientSideCourseShortDetailsViewModel> coursesInCart)
        {
            model.Id = cart.Id;
            model.UserId = cart.UserId;
            model.Items = cart.Items?.Select(item => new CartItemViewModel
            {
                Id = item.Id,
                CartId = item.CartId,
                CourseId = item.CourseId,
                Course = coursesInCart.First(c => c.Id == item.CourseId)
            }).ToList();

            model.TotalAmount = model.Items?.Sum(item => item.Course.Price ?? 0) ?? 0;

            return model;
        }
    }
}
