using Barnamenevisan.Localizing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Interfaces.CourseRequest
{
    public interface ICourseRequestVoteRepository : IRepository<Models.CourseRequest.CourseRequestVote, int>
    {
    }
}
