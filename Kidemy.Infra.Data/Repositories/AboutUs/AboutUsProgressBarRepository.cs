using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.AboutUs;
using Kidemy.Domain.Models.AboutUs;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.AboutUs
{
    public class AboutUsProgressBarRepository : EfRepository<AboutUsProgressBar, int>, IAboutUsProgressBarRepository
    {
        #region Ctor
        public AboutUsProgressBarRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
