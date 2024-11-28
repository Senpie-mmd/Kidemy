using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IVIPMembersService
    {
        Task<Result> JoinVIPMembersAsync(ClientSideJoinVIPMembersViewModel model);
        Task<Result<ClientSideVIPUserInformationViewModel>> GetVIPUserInformation(int userId);
        Task<Result<bool>> IsUserVIPMemberAsync(int userId);
        Task<Result> ExpirationVIPAccountAsync(int userId);
    }
}
