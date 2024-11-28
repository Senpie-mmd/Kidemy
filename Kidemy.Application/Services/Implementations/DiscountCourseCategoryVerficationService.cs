using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Domain.Models.Cart;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Models.Discount;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountCourseCategoryVerficationService : IDiscountVerificationService
    {
        private readonly ICourseService _courseService;

        public DiscountCourseCategoryVerficationService(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public bool IsDiscountValid(CourseFullDetailsViewModel course, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            //check if a category exists within limitations
            var isValid = true;

            var limitedCategories = discount.DiscountLimitations.SelectMany(dl => dl.Categories).ToList();
            
            if (limitedCategories?.Any() ?? false)
            {
                isValid = course.Categories.Any(courseCategory => limitedCategories.Any(limitedCategory => courseCategory.Id == limitedCategory.CategoryId));

                if (isValid) return true;

                var subCategoriesResult = _courseService.GetCategoriesWithSubCategoriesAsync(limitedCategories.Select(c => c.CategoryId).ToList()).Result;

                if (subCategoriesResult.IsFailure) return false;
                
                var subCategories = subCategoriesResult.Value;

                while (true)
                {
                    try
                    {
                        var categoryIdsInDiscount = subCategories?.SelectMany(c => c.SubCategories ?? new List<CourseCategoryWithSubCategoriesViewModel>())
                                                                 ?.Select(c => c.Id);

                        isValid = course.Categories.Any(courseCategory => categoryIdsInDiscount?.Contains(courseCategory.Id) ?? false);

                        categoryIdsInDiscount = subCategories?.SelectMany(c => c.SubCategories ?? new List<CourseCategoryWithSubCategoriesViewModel>())
                                                             ?.SelectMany(c => c.SubCategories ?? new List<CourseCategoryWithSubCategoriesViewModel>())
                                                             ?.Select(c => c.Id);

                        isValid = isValid || course.Categories.Any(courseCategory => categoryIdsInDiscount?.Contains(courseCategory.Id) ?? false);

                        if (isValid) return true;
                    }
                    catch
                    {
                        return isValid;
                    }

                    var categoryIdsToGetSubCategories = subCategories?.SelectMany(c => c.SubCategories ?? new List<CourseCategoryWithSubCategoriesViewModel>())
                                                                     ?.SelectMany(c => c.SubCategories ?? new List<CourseCategoryWithSubCategoriesViewModel>())
                                                                     ?.Select(c => c.Id)
                                                                     ?.ToList() 
                                                                     ?? new List<int>();

                    if(categoryIdsToGetSubCategories?.Count == 0) return isValid;

                    subCategoriesResult = _courseService.GetCategoriesWithSubCategoriesAsync(categoryIdsToGetSubCategories).Result;

                    if (subCategoriesResult.IsFailure) return isValid;

                    subCategories = subCategoriesResult.Value;
                }
            }
            else
            {
                return isValid;
            }
        }

        public bool IsDiscountValid(CartViewModel cart, Discount discount, int userId, string? enteredDiscountCode = null)
        {
            var limitedCategories = discount.DiscountLimitations.SelectMany(dl => dl.Categories).ToList();

            if (limitedCategories?.Any() ?? false)
            {
                if (cart.Items?.Count == 0) return false;

                var coursesFullDetailsResult = _courseService.GetCoursesFullDetailsByIdsAsync(cart.Items.Select(item => item.CourseId).ToList()).Result;

                if (coursesFullDetailsResult.IsFailure) return false;

                foreach (var course in coursesFullDetailsResult.Value)
                {
                    var isValid = IsDiscountValid(course, discount, userId, enteredDiscountCode);

                    if (isValid) return true;
                }

                // not found any course with expected categories
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
