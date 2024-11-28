using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Master;

namespace Kidemy.Domain.Models.Master
{
    /// <summary>
    /// Master id is equal to user id
    /// </summary>
    public class Master : AuditBaseEntity<int>
    {
        public string? Bio { get; set; }
        public string? FatherName { get; set; }
        public string? NationalIDNumber { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? PlaceOfIssuance { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? IdentificationFileName { get; set; }
        public MasterStatus Status { get; set; }
        public decimal? BlockedAmount { get; set; }

        #region Relations

        public ICollection<UploadedMasterContract> UploadedMasterContracts { get; set; }

        public Models.User.User User { get; set; }

        #endregion
    }
}
