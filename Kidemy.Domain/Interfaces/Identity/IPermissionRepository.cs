using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Identity;

namespace Kidemy.Domain.Interfaces.Identity
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
    }
}
