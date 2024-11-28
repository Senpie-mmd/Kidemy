using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public class ClientSideGetSlugsViewModel : BaseEntityViewModel<int>
    {
        public string Slugs { get; set; }
        public string Title { get; set; }
    }
}
