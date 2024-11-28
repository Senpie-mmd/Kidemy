using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course
{
    public class CourseShortDetailsViewModel : BaseEntityViewModel<int>
    {
        public string ImageFileName { get; set; }

        [Translate(EntityName = "Course")]
        public string Title { get; set; }

        public string Slug { get; set; }
        
        public decimal Price { get; set; }
    }
}
