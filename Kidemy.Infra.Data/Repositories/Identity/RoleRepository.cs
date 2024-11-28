using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Identity;
using Kidemy.Domain.Models.Identity;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Identity
{
    public class RoleRepository : EfRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(KidemyContext context) : base(context)
        {
        }


    }
}
