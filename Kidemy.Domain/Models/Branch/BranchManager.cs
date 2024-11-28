using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Branch
{
    public class BranchManager : AuditBaseEntity<int>
    {
        public int BranchId { get; set; }

        public int UserId { get; set; }

        public Branch Branch { get; set; }
    }
}
