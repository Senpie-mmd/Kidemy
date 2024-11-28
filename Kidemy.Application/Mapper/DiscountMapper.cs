using Kidemy.Application.Convertors;
using Kidemy.Application.ViewModels.Discount.AdminSide;
using Kidemy.Application.ViewModels.Discount.ClientSide;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Models.Discount;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class DiscountMapper
    {
        public static Expression<Func<Discount, AdminSideDiscountViewModel>> MapDiscountViewModel => (Discount discount) =>
           new AdminSideDiscountViewModel
           {
               Id = discount.Id,
               Title = discount.Title,
               Code = discount.Code,
               Type = discount.Type,
               AutoUse = discount.AutoUse,
               IsActive = discount.IsActive,
               EndDateTimeOnUtc = discount.EndDateTimeOnUtc,
               StartDateTimeOnUtc = discount.StartDateTimeOnUtc,
               IsPercentage = discount.IsPercentage,
               Value = discount.Value
           };

        public static AdminSideUpsertDiscountViewModel MapFrom(this AdminSideUpsertDiscountViewModel model, Discount discount)
        {
            model.Id = discount.Id;
            model.Title = discount.Title;
            model.Code = discount.Code;
            model.Type = discount.Type;
            model.AutoUse = discount.AutoUse;
            model.IsActive = discount.IsActive;
            model.EndDateTime = discount.EndDateTimeOnUtc?.ToUserShortDateTime();
            model.StartDateTime = discount.StartDateTimeOnUtc?.ToUserShortDateTime();
            model.IsPercentage = discount.IsPercentage;
            model.Value = discount.Value;

            return model;
        }

        public static ClientSideDiscountViewModel MapFrom(this ClientSideDiscountViewModel model, Discount discount)
        {
            model.Id = discount.Id;
            model.Code = discount.Code;
            model.Type = discount.Type;
            model.AutoUse = discount.AutoUse;
            model.IsActive = discount.IsActive;
            model.EndDateTimeOnUtc = discount.EndDateTimeOnUtc;
            model.StartDateTimeOnUtc = discount.StartDateTimeOnUtc;
            model.IsPercentage = discount.IsPercentage;
            model.Value = discount.Value;

            return model;
        }

        public static AdminSideUpsertDiscountLimitationViewModel MapFrom
            (this AdminSideUpsertDiscountLimitationViewModel model,
                    DiscountLimitation discountLimitation,
                    List<UserFullNameModel>? userFullNameModels,
                    List<CourseTitleModel>? courseTitleModels,
                    List<CourseCategoryTitleModel>? courseCategoryTitleModels)
        {
            model.Id = discountLimitation.Id;
            model.DiscountId = discountLimitation.DiscountId;
            model.Type = discountLimitation.Type;

            model.Users = new List<AdminSideDiscountLimitationUserMappingViewModel>()
                .MapFrom(discountLimitation.Users.ToList());

            model.Courses = new List<AdminSideDiscountLimitationCourseMappingViewModel>()
                .MapFrom(discountLimitation.Courses.ToList());

            model.Categories = new List<AdminSideDiscountLimitationCategoryMappingViewModel>()
                .MapFrom(discountLimitation.Categories.ToList());

            model.UsageCount = discountLimitation.UsageCount is null
                ? null
                : new AdminSideDiscountLimitationUsageCountMappingViewModel().MapFrom(discountLimitation.UsageCount);

            if (userFullNameModels?.Any() ?? false)
            {
                foreach (var item in model.Users)
                {
                    item.UserName = userFullNameModels.FirstOrDefault(u => u.UserId == item.UserId)?.UserFullName ?? "-";
                }
            }

            if (courseTitleModels?.Any() ?? false)
            {
                foreach (var item in model.Courses)
                {
                    item.CourseTitle = courseTitleModels.FirstOrDefault(u => u.CourseId == item.CourseId)?.Title ?? "-";
                }
            }

            if (courseCategoryTitleModels?.Any() ?? false)
            {
                foreach (var item in model.Categories)
                {
                    item.CategoryTitle = courseCategoryTitleModels.FirstOrDefault(u => u.CategoryId == item.CategoryId)?.Title ?? "-";
                }
            }

            return model;
        }

        public static AdminSideDiscountLimitationUserMappingViewModel MapFrom(this AdminSideDiscountLimitationUserMappingViewModel model, DiscountLimitationUserMapping discountLimitationUserMapping)
        {
            model.DiscountLimitationId = discountLimitationUserMapping.DiscountLimitationId;
            model.UserId = discountLimitationUserMapping.UserId;

            return model;
        }

        public static List<AdminSideDiscountLimitationUserMappingViewModel> MapFrom(this List<AdminSideDiscountLimitationUserMappingViewModel> model, List<DiscountLimitationUserMapping> discountLimitationUserMappings)
        {
            return discountLimitationUserMappings.Select(u => new AdminSideDiscountLimitationUserMappingViewModel().MapFrom(u)).ToList();
        }

        public static AdminSideDiscountLimitationCourseMappingViewModel MapFrom(this AdminSideDiscountLimitationCourseMappingViewModel model, DiscountLimitationCourseMapping discountLimitationCourseMapping)
        {
            model.DiscountLimitationId = discountLimitationCourseMapping.DiscountLimitationId;
            model.CourseId = discountLimitationCourseMapping.CourseId;

            return model;
        }

        public static List<AdminSideDiscountLimitationCourseMappingViewModel> MapFrom(this List<AdminSideDiscountLimitationCourseMappingViewModel> model, List<DiscountLimitationCourseMapping> discountLimitationCourseMappings)
        {
            return discountLimitationCourseMappings.Select(u => new AdminSideDiscountLimitationCourseMappingViewModel().MapFrom(u)).ToList();
        }

        public static AdminSideDiscountLimitationCategoryMappingViewModel MapFrom(this AdminSideDiscountLimitationCategoryMappingViewModel model, DiscountLimitationCategoryMapping discountLimitationCategoryMapping)
        {
            model.DiscountLimitationId = discountLimitationCategoryMapping.DiscountLimitationId;
            model.CategoryId = discountLimitationCategoryMapping.CategoryId;

            return model;
        }

        public static List<AdminSideDiscountLimitationCategoryMappingViewModel> MapFrom(this List<AdminSideDiscountLimitationCategoryMappingViewModel> model, List<DiscountLimitationCategoryMapping> discountLimitationCategoryMappings)
        {
            return discountLimitationCategoryMappings.Select(u => new AdminSideDiscountLimitationCategoryMappingViewModel().MapFrom(u)).ToList();
        }

        public static AdminSideDiscountLimitationUsageCountMappingViewModel MapFrom(this AdminSideDiscountLimitationUsageCountMappingViewModel model, DiscountLimitationUsageCountMapping discountLimitationUsageCount)
        {
            model.DiscountLimitationId = discountLimitationUsageCount.DiscountLimitationId;
            model.Count = discountLimitationUsageCount.Count;

            return model;
        }
    }
}
