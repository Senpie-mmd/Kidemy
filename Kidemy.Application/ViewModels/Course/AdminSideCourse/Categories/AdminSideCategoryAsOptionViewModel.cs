using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories
{
    public class AdminSideCategoryAsOptionViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(CourseCategory))]
        public string Title { get; set; }
    }
}
