using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseViewRepository : EfRepository<CourseView, int>, ICourseViewRepository
    {
        public CourseViewRepository(KidemyContext context) : base(context)
        {
        }
    }
}
