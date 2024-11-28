using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.AboutUs;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.AboutUs
{
    public class AboutUsRepository : EfRepository<Kidemy.Domain.Models.AboutUs.AboutUs, int>, IAboutUsRepository

    {
        public AboutUsRepository(KidemyContext context) : base(context)
        {

        }
    }
}
