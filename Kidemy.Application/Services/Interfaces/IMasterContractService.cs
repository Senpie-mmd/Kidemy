using Kidemy.Application.ViewModels.MasterContract;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IMasterContractService
    {
        Task<Result<FilterMasterContractViewModel>> FilterAsync(FilterMasterContractViewModel model);
        Task<Result<AdminSideUpdateMasterContractViewModel>> GetMasterContractForEditByIdAsync(int id);
        Task<Result> CreateAsync(AdminSideCreateMasterContractViewModel model);
        Task<Result> UpdateAsync(AdminSideUpdateMasterContractViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<List<ClientSideMasterContractDetailsViewModel>>> GetMasterContractsAsync();
        Task<Result<List<ClientSideUploadedMasterContractDetailsViewModel>>> GetUploadedMasterContractsAsync(int masterId);
      
    }
}
