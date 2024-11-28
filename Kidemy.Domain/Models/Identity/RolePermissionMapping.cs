using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.Identity
{
    public class RolePermissionMapping
    {
        #region Properties

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        #endregion

        #region Relations


        public Role Role { get; set; }


        public Permission Permission { get; set; }

        #endregion
    }
}
