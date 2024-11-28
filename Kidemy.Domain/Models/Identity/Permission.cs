using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Identity
{
    public class Permission : BaseEntity<int>
    {
        public string UniqueName { get; set; }

        public int? ParentId { get; set; }

        public ICollection<RolePermissionMapping> Roles { get; set; }
    }
}
