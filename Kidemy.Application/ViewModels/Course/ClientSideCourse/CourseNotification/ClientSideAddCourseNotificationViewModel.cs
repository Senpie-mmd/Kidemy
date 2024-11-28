using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.CourseNotification;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseNotification
{
    public class ClientSideAddCourseNotificationViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public CourseNotificationType Type { get; set; }
    }
}
