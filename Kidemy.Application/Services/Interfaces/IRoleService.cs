using Kidemy.Domain.Shared;
using Kidemy.Application.ViewModels.Role;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Result> CreateRoleAsync(AdminSideUpsertRoleViewModel model);
        Task<Result> DeleteRoleAsync(int id);
        Task<Result<FilterRoleViewModel>> FilterRolesAsync(FilterRoleViewModel model);
        Task<Result<List<AdminSideRoleViewModel>>> GetAllRolesAsync();
        Task<Result<AdminSideUpsertRoleViewModel>> GetRoleById(int id);
        Task<Result> UpdateRoleAsync(AdminSideUpsertRoleViewModel model);
        Task<Result<bool>> UserHasPermissionAsync(int userId, string permission);
        Task<Result<bool>> UserIsMasterNotAsync(int userId);
        Task<bool> BoolUserHasPermissionAsync(int userId, string permission);

    }
}
