using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories
{
    public class AdminSideCategoryViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(CourseCategory))]
        public string Title { get; set; }

        public int? ParentId { get; set; }
        
        public string LogoImageName { get; set; }

        [Translate(EntityName = nameof(CourseCategory), Key = "Title", PropertyNameOfEntityIdInThisClass = nameof(ParentId))]
        public string? ParentTitle { get; set; }
    }
}
