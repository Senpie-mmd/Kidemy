using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Interfaces.Banner;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Banner
{
    public class BannerRepository : EfRepository<Domain.Models.Banner.Banner, int>, IBannerRepository
    {
        public BannerRepository(KidemyContext context) : base(context)
        {

        }

    }
}
