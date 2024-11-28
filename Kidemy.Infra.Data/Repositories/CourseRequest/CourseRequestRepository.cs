using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.CourseRequest;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.CourseRequest
{
    public class CourseRequestRepository : EfRepository<Domain.Models.CourseRequest.CourseRequest, int>, ICourseRequestRepository
    {
        #region Constructor
        public CourseRequestRepository(KidemyContext context) : base(context)
        {

        }
        #endregion

        public async Task<int> GetCountAsync(Expression<Func<Domain.Models.CourseRequest.CourseRequest, bool>>? expression = null)
            => expression == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(expression);

    }
}
