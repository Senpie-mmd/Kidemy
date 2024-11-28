using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region User
        Task<Result> CreateUserAsync(AdminSideUpsertUserViewModel model);
        Task<Result> DeleteUserByAdminAsync(int? id);
        Task<Result> UnableToWithdrawUser(int? id);
        Task<Result> AbleToWithdrawUser(int? id);
        Task<Result<bool>> MasterHasAccessToWithraw(int? id);
        Task<Result<FilterUsersViewModel>> FilterUsersAsync(FilterUsersViewModel model);
        Task<Result<ClientSideUserViewModel>> GetUserByMobileAsync(string mobile);
        Task<Result<ClientSideUserViewModel>> GetUserByMobileAsync(int id);
        Task<int> GetUserCount();
        Task<Result<AdminSideUserDetailsViewModel>> GetUserInfoAsync(int id);
        Task<Result<bool>> IsUserBan(int userId);
        Task<Result> UpdateUserPassword(int userid, ChangePasswordViewModel password);
        Task<Result> UpdateUserByAdminAsync(AdminSideUpsertUserViewModel model);
        Task<List<UserFullNameModel>> GetUsersFullNames(List<int> userIds);
        Task<Result> RegisterUserAsync(RegisterViewModel model);
        Task<Result<string?>> GetUserEmailByIdAsync(int id);
        Task<Result<string?>> GetUserMobileByIdAsync(int id);
        Task<Result<string?>> GetUserAvatarNameByIdAsync(int id);
        Task<Result<string>> GetUserFullName(int id);
        Task<Result> ConfirmUserMobileAsync(ConfirmMobileViewModel model);
        Task<Result> SendMobileConfirmationCodeAsync(string mobile);
        Task<Result<ClientSideUserViewModel>> ConfirmLogin(LoginViewModel model);
        Task<Result> ResetPassword(string mobile, string newPassword);
        Task<Result<AdminSideUpsertUserViewModel>> GetUserForEditInAdminSideByIdAsync(int id);
        Task<Result<ClientSideUpdateUserViewModel>> GetUserProfileForEditByIdAsync(int id);
        Task<Result> UpdateUserProfileAsync(ClientSideUpdateUserViewModel model);
        Task<Result> ConfirmUserEmailAsync(string confirmationCode);
        Task<Result<List<int>>> GetUsersIds();
        Task<Result<List<string>>> GetUsersEmails();
        Task<Result<List<string>>> GetUsersNumbers();
        Task<Result<List<ClientSideUserNameAndUserProfileViewModel>>> GetUserNameAndUserProfileByUserId(List<int> id);
        Task<Result<List<AdminSideUserNameAndMobileForMasterModel>>> GetUserNameAndMobileForMasterByUserId(List<int> id);
        Task<Result<List<Domain.Models.User.User>>> GetUserIdByuserName(string userName);
        Task<Result<bool>> UserIsMasterAsync(int userId);
        Task<Result<bool>> IsUserVIPMemberAsync(int userId);
        Task<Result<List<UserFullNameModel>>> GetMastersListAsync();
        Task<Result<int>> GetMastersCountAsync();
        Task<Result<int>> GetTodayUserCountAsync();
        Task<List<UserNameAndUserProfileModel>> GetUsersProfileDetails(List<int> userIds);
        Task<Result<bool>> UserIsAdminAsync(int userId);
        Task<Result<bool>> ExistsUser(string mobile);

        #endregion

    }
}
