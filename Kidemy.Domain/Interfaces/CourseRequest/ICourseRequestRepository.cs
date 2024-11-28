using Barnamenevisan.Localizing.Repository;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.CourseRequest
{
    public interface ICourseRequestRepository : IRepository<Models.CourseRequest.CourseRequest, int>
    {
        Task<int> GetCountAsync(Expression<Func<Models.CourseRequest.CourseRequest, bool>> expression);
    }
}
