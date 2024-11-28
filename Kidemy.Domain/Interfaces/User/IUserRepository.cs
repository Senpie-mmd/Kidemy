using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.User;
using System.Linq.Expressions;

namespace Kidemy.Domain.Interfaces.User
{
    public interface IUserRepository : IRepository<Kidemy.Domain.Models.User.User, int>
    {
        Task<List<UserFullNameModel>> GetUsersFullNames(List<int> userIds);
        Task<string?> GetUserEmailByIdAsync(int id);
        Task<string?> GetUserAvatarNameByIdAsync(int id);
        Task<int> GetUserCount(Expression<Func<Domain.Models.User.User, bool>>? expression = null);
        Task<string?> GetUserMobileByIdAsync(int id);
        Task<string> GetUserFullName(int userId);
        Task<List<string>> GetUsersEmails();
        Task<List<int>> GetUsersIds();
        Task<List<string>> GetUsersMobiles();
        Task<List<UserNameAndUserProfileModel>> GetUserNamesAndUserProfilesById(List<int> id);
        Task<List<AdminSideUserNameAndMobileForMasterModel>> GetUserNameAndMobileForMasterByUserId(List<int> id);
        Task<List<UserFullNameModel>> GetUsersThatHaveMasterRoleList();
        Task<int> GetMastersCountAsync();
    }
}
