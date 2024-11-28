using Barnamenevisan.Localizing.Repository;

namespace Kidemy.Domain.Interfaces.VIPMembers
{
    public interface IVIPMemberReposirory : IRepository<Kidemy.Domain.Models.VIPMembers.VIPMember, int>
    {
        Task<DateTime> GetUserLastMembershipEndDateAsync(int userId);
    }
}
