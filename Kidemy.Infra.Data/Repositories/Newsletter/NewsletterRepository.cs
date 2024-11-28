using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Newsletter;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Newsletter
{
    public class NewsletterRepository : EfRepository<Domain.Models.Newsletter.Newsletter, int>, INewsletterRepository
    {
        #region Constructor
        public NewsletterRepository(KidemyContext context) : base(context)
        {

        }
        #endregion
    }
}
