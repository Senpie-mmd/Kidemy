using AngleSharp.Dom;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Order;
using Kidemy.Domain.Models.Order;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class OrderMapper
    {
        public static Expression<Func<Order, AdminSideOrderViewModel>> MapAdminSideOrderViewModel => (Order order) =>
        new AdminSideOrderViewModel
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount,
            DiscountedTotalAmount = order.DiscountedTotalAmount,
            CreatedDateOnUtc = order.CreatedDateOnUtc,
            IsPaid = order.IsPaid
        };

        public static Expression<Func<Order, ClientSideOrderViewModel>> MapClientSideOrderViewModel => (Order order) =>
        new ClientSideOrderViewModel
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount,
            DiscountedTotalAmount = order.DiscountedTotalAmount,
            CreatedDateOnUtc = order.CreatedDateOnUtc,
            IsPaid = order.IsPaid,
        };

        public static ClientSideOrderDetailsViewModel MapFrom(this ClientSideOrderDetailsViewModel model, Order order, List<CourseShortDetailsViewModel> coursesInOrder)
        {
            model.Id = order.Id;
            model.CreatedDateOnUtc = order.CreatedDateOnUtc;
            model.TotalAmount = order.TotalAmount;
            model.DiscountedTotalAmount = order.DiscountedTotalAmount;
            model.IsPaid = order.IsPaid;
            model.UserId = order.UserId;
            model.Items = order.Items.Select(item => new ClientSideOrderItemViewModel().MapFrom(item, coursesInOrder.First(c => c.Id == item.CourseId))).ToList();

            return model;
        }

        public static ClientSideOrderItemViewModel MapFrom(this ClientSideOrderItemViewModel model, OrderItem orderItem, CourseShortDetailsViewModel course)
        {
            model.Id = orderItem.Id;
            model.OrderId = orderItem.Id;
            model.CourseId = orderItem.CourseId;
            model.UnitPrice = orderItem.UnitPrice;
            model.DiscountedUnitPrice = orderItem.DiscountedUnitPrice;
            model.Course = course;

            return model;
        }

        public static AdminSideOrderDetailsViewModel MapFrom(this AdminSideOrderDetailsViewModel model, Order order, List<CourseShortDetailsViewModel> coursesInOrder, string userName)
        {
            model.Id = order.Id;
            model.CreatedDateOnUtc = order.CreatedDateOnUtc;
            model.TotalAmount = order.TotalAmount;
            model.DiscountedTotalAmount = order.DiscountedTotalAmount;
            model.IsPaid = order.IsPaid;
            model.UserId = order.UserId;
            model.UserName = userName;
            model.Items = order.Items.Select(item => new AdminSideOrderItemViewModel().MapFrom(item, coursesInOrder.First(c => c.Id == item.CourseId))).ToList();

            return model;
        }

        public static AdminSideOrderItemViewModel MapFrom(this AdminSideOrderItemViewModel model, OrderItem orderItem, CourseShortDetailsViewModel course)
        {
            model.Id = orderItem.Id;
            model.OrderId = orderItem.Id;
            model.CourseId = orderItem.CourseId;
            model.UnitPrice = orderItem.UnitPrice;
            model.DiscountedUnitPrice = orderItem.DiscountedUnitPrice;
            model.Course = course;

            return model;
        }
    }
}
