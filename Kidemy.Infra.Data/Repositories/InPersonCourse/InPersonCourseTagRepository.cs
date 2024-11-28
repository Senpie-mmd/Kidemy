using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.InPersonCourse;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.InPersonCourse
{
    public class InPersonCourseTagRepository : EfRepository<Domain.Models.InPersonCourse.InPersonCourseTag, int>, IInPersonCourseTagRepository
    {
        #region Constructor
        public InPersonCourseTagRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
