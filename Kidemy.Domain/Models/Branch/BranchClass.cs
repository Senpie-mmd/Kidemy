using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Branch
{
    public class BranchClass : AuditBaseEntity<int>
    {
        public int BranchId { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public Branch Branch { get; set; }
    }
}
