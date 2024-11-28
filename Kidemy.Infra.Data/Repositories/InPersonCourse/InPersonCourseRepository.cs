using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.InPersonCourse;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.InPersonCourse
{
    public class InPersonCourseRepository : EfRepository<Domain.Models.InPersonCourse.InPersonCourse, int>, IInPersonCourseRepository
    {
        #region Constructor
        public InPersonCourseRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
