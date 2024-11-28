using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse
{
    public class CourseCategoryWithSubCategoriesViewModel : BaseEntityViewModel<int>
    {
        public string Title { get; set; }

        public int? ParentCourseCategoryId { get; set; }

        public List<CourseCategoryWithSubCategoriesViewModel> SubCategories { get; set; }
    }
}
