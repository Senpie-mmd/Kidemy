using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.CourseRequest;
using Kidemy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.CourseRequest
{
    public class CourseRequestMasterVolunteerRepository : EfRepository<Domain.Models.CourseRequest.CourseRequestMasterVolunteer, int>, ICourseRequestMasterVolunteerRepository
    {
        #region Constructor
        public CourseRequestMasterVolunteerRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
