using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class FavouriteCourseRepository : EfRepository<FavouriteCourse, int>, IFavouriteCourseRepository
    {
        public FavouriteCourseRepository(KidemyContext context) : base(context)
        {
        }
    }
}
