using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Discount.AdminSide;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<Result> ApplyDiscount(List<ClientSideCourseShortDetailsViewModel> courseViewModels, string? code = null);
        Task<Result> ApplyDiscount(ClientSideCourseDetailViewModel courseViewModel, string? code = null);
        Task<Result> ApplyDiscount(CartViewModel cartViewModel, string? code = null);
        Task<Result> ApplyDiscount(List<ClientSideMastersOtherCoursesViewModel> courseViewModels, string? code = null);
        Task<Result> ApplyDiscount(List<ClientSideHomePageOfferedCoursesViewModel> courseViewModels, string? code = null);
        Task<Result> ApplyDiscount(List<ClientSideCourseViewModel> courseViewModels, string? code = null);
        Task<Result> ApplyDiscount(List<ClientSideLastCoursesViewModel> courseViewModels, string? code = null);
        Task<Result> CreateAsync(AdminSideUpsertDiscountViewModel model);
        Task<Result> CreateDiscountLimitationAsync(AdminSideUpsertDiscountLimitationViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result> DeleteDiscountLimitationAsync(int id);
        Task<Result<bool>> DiscountIsUsedByUser(int discountId, int userId);
        Task<Result<AdminSideFilterDiscountLimitationViewModel>> FilterDiscountLimitationAsync(AdminSideFilterDiscountLimitationViewModel model);
        Task<Result<AdminSideFilterDiscountViewModel>> FilterForAdminAsync(AdminSideFilterDiscountViewModel model);
        Task<Result<AdminSideUpsertDiscountViewModel>> GetByIdForEditByAdminAsync(int id);
        Task<Result<AdminSideUpsertDiscountLimitationViewModel>> GetDiscountLimitationForUpdateByAdmin(int id);
        Task<Result> UpdateAsync(AdminSideUpsertDiscountViewModel model);
        Task<Result> UpdateDiscountLimitationAsync(AdminSideUpsertDiscountLimitationViewModel model);
    }
}