using Kidemy.Domain.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kidemy.Domain.Models.User
{
	public class UserRoleMapping
	{
        #region Properties

        public int UserId { get; set; }

        public int RoleId { get; set; }

        #endregion

        #region Relations
        
        public User User { get; set; }

        #endregion
    }
}
