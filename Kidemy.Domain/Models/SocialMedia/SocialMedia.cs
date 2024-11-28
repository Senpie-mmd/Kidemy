using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.SocialMedia
{
    public class SocialMedia : AuditBaseEntity<int>
    {
        #region Properties

        public int Priority { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string ImageName { get; set; }

        #endregion
    }
}