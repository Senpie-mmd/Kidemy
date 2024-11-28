using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseCategoryMapper
    {
        public static AdminSideCategoryAsOptionViewModel MapFrom(this AdminSideCategoryAsOptionViewModel model, CourseCategory category)
        {
            model.Id = category.Id;
            model.Title = category.Title;

            return model;
        }

        public static Expression<Func<CourseCategory, AdminSideCategoryViewModel>> MapAdminSideCategoryViewModel => (category) => new AdminSideCategoryViewModel
        {
            Id = category.Id,
            LogoImageName = category.LogoFileName,
            ParentId = category.ParentCourseCategoryId,
            Title = category.Title,
            ParentTitle = category.ParentCategory.Title ?? "-"
        };

        public static AdminSideUpdateCategoryViewModel MapFrom(this AdminSideUpdateCategoryViewModel model, CourseCategory category)
        {
            model.Id = category.Id;
            model.Title = category.Title;
            model.ImageName = category.LogoFileName;
            model.ParentCategoryId = category.ParentCourseCategoryId;
            model.IsPopular = category.IsPopular;

            return model;
        }
    }
}
