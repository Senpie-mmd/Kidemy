using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Master
{
    public class MasterContractRepository : EfRepository<Domain.Models.Master.MasterContract, int>, IMasterContractRepository
    {
        public MasterContractRepository(KidemyContext context) : base(context)
        {

        }
    }
}
