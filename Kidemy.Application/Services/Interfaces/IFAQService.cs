using Kidemy.Application.ViewModels.FAQ;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IFAQService
    {
        Task<Result<FilterFAQViewModel>> FilterAsync(FilterFAQViewModel model);
        Task<Result<AdminSideUpsertFAQViewModel>> GetFAQForEditByIdAsync(int id);
        Task<Result> CreateAsync(AdminSideUpsertFAQViewModel model);
        Task<Result> UpdateAsync(AdminSideUpsertFAQViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<List<ClientSideFAQViewModel>>> GetAllFAQs();
    }
}
