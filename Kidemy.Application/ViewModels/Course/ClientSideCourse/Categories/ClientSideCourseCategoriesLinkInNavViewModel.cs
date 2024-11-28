using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories
{
    public class ClientSideCourseCategoriesLinkInNavViewModel : BaseEntityViewModel<int>
    {
        public int? ParentCategoryId { get; set; }

        [Translate(EntityName = nameof(CourseCategory))]
        public string Title { get; set; }

        public List<ClientSideCourseCategoriesLinkInNavViewModel>? SubCategories { get; set; }
    }
}