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
    public class CourseRequestVoteRepository : EfRepository<Domain.Models.CourseRequest.CourseRequestVote, int>, ICourseRequestVoteRepository
    {
        #region Constructor
        public CourseRequestVoteRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
