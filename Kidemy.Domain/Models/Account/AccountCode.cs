using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Account
{
    public class AccountCode : BaseEntity<int>
    {
        #region Properties

        public string Receiver { get; set; }

        public string Code { get; set; }

        #endregion

        public DateTime ExpiryDateTimeUtc { get; set; }


    }
}
