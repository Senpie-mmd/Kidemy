using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IPageService
    {
        Task<Result<FilterPageViewModel>> FilterAsync(FilterPageViewModel model);
        Task<Result<AdminSideUpsertPageViewModel>> GetPageForEditByIdAsync(int id);
        Task<Result> CreateAsync(AdminSideUpsertPageViewModel model);
        Task<Result> UpdateAsync(AdminSideUpsertPageViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<ClientSidePageViewModel>> GetPageForUserBySlug(string slug);
        Task<Result<List<ClientSidePageViewModel>>> GetAllPagesForHeader();
        Task<Result<List<ClientSidePageViewModel>>> GetAllPagesForFooter();
    }
}
