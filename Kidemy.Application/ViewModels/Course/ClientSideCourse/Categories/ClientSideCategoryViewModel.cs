using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories
{
    public class ClientSideCategoryViewModel : BaseEntityViewModel<int>
    {
        public string imageName { get; set; }
        public string Title { get; set; }
        public int? CoursesCount { get; set; }
    }
}
