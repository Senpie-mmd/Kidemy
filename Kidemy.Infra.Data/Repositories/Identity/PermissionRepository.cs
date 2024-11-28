using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Identity;
using Kidemy.Domain.Models.Identity;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Identity
{
    public class PermissionRepository : EfRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(KidemyContext context) : base(context)
        {
        }
    }
}
