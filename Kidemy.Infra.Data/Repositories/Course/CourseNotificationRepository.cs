using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseNotificationRepository : EfRepository<Domain.Models.Course.CourseNotification, int>, ICourseNotificationRepository
    {
        public CourseNotificationRepository(KidemyContext context) : base(context)
        {
        }
    }
}
