using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IUploadedMasterContractService
    {
        Task<Result> CreateAsync(ClientSideCreateUploadedMasterContractViewModel model);
        Task<Result> UpdateAsync(ClientSideUpdateUploadedMasterContractViewModel model);
        Task<Result> UpdateAsync(List<AdminSideUpdateUploadedMasterContractViewModel> model);
        Task<Result<List<ClientSideUploadedMasterContractDetailsViewModel>>> GetUploadMasterContractsAsync(int masterId);
        Task<Result<bool>> ExistUploadedMasterContractByIdAsync(int masterContractId, int masterId);
        Task<Result<int>> GetUploadedMasterContractIdByMasterContractIdAsync(int masterContractId);
        Task<Result<FilterMasterInformationForContractsPendingConfirmationViewModel>> FilterAsync(FilterMasterInformationForContractsPendingConfirmationViewModel model);
        Task<Result<List<AdminSideMasterContractsPendingConfirmationDetailViewModel>>> GetMasterContractsPendingConfirmationListByMasterIdAsync(int masterId);
       
    }
}
