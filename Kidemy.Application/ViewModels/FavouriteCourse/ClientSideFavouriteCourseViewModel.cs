using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.FavouriteCourse
{
    public class ClientSideFavouriteCourseViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public string? Slug { get; set; }
        public string? CourseTitle { get; set; }

    }
}
