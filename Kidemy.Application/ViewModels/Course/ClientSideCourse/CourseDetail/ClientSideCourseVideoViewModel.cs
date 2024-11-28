using Barnamenevisan.Localizing.Shared;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideCourseVideoViewModel : BaseEntityViewModel<int>
    {
        public string ThumbnailImageName { get; set; }
        public string Title { get; set; }
        public TimeSpan VideoLength { get; set; }
        public bool IsFree { get; set; }
        public int Priority { get; set; }
        public bool IsPublished { get; set; }
        public bool HasAttachmentFile { get; set; }
        public string VideoCategoryTitle { get; set; }
    }
}
