using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.DTOs.Master;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IMasterService
    {
        Task<Result> CreateAsync(ClientSideRegisterMasterViewModel model);
        Task<Result> UpdateAsync(ClientSideUpdateMasterViewModel model);
        Task<Result<ClientSideUpdateMasterViewModel>> GetMasterForEditByIdAsync(int id);
        Task<Result<bool>> IsExistMasterByIdAsync(int id);
        Task<Result> SetSetBlockedAmount(BlockedAmountViewModel model);
        Task<Result<FilterForAdminSideMasterViewModel>> FilterForAdminSideAsync(FilterForAdminSideMasterViewModel model);
        Task<Result<AdminSideMasterDetailsViewModel>> GetMasterInfoAsync(int id);
        Task<Result> ChangeStatusAsync(int id, MasterStatus masterStatus);
        Task<Result> CreateAsync(int id);
        Task<Result<List<ClientSideBioForMasterModel>>> GetMasterBioByUserId(List<int> ids);
        Task<Result<BlockedAmountViewModel>> GetBlockedBalanceAsync(int userId);
        Task<Result<FilterForClientSideMasterViewModel>> FilterForClientSideAsync(FilterForClientSideMasterViewModel model);
        Task<Result<ClientSideMasterDetailsViewModel>> GetMasterDetailsByIdAsync(int id);
        Task<Result<List<int>>> GetMastersIds();
        Task<Result<bool>> MasterIsConfirmed(int id);
        Task<Result<int>> GetCountAsync(MasterStatus status);
    }
}
