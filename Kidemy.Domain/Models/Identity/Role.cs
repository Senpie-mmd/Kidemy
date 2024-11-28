using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Identity
{
    public class Role : AuditBaseEntity<int>
    {
        public string Title { get; set; }

        public string UniqueName { get; set; }


        public ICollection<RolePermissionMapping> Permissions { get; set; }


    }
}
