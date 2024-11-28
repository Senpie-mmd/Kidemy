using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.SocialMedia;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.SocialMedia
{
    public class SocialMediaRepository : EfRepository<Kidemy.Domain.Models.SocialMedia.SocialMedia, int>, ISocialMediaRepository
    {
        #region Constructor
        public SocialMediaRepository(KidemyContext context) : base(context)
        {
        }

        #endregion
    }
}