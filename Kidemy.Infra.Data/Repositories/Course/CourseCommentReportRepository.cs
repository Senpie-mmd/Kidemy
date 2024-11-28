using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseCommentReportRepository : EfRepository<CourseCommentReport, int>, ICourseCommentReportRepository
    {
        public CourseCommentReportRepository(KidemyContext context) : base(context)
        {
        }
    }
}
