using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Models.User;

namespace Kidemy.Domain.Models.Master
{
    public class UploadedMasterContract : AuditBaseEntity<int>
    {
        public int MasterId { get; set; }
        public int MasterContractId { get; set; }
        public UploadedMasterContractStatus Status { get; set; }
        public string FileName { get; set; }

        #region Relations

        public Master Master { get; set; }
        public MasterContract MasterContract { get; set; }

        #endregion

    }
}
