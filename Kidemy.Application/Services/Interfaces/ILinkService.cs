using Kidemy.Application.ViewModels.Link;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ILinkService
    {
        Task<Result<FilterLinkViewModel>> FilterAsync(FilterLinkViewModel model);
        Task<Result<AdminSideUpsertLinkViewModel>> GetLinkForEditByIdAsync(int id);
        Task<Result> CreateAsync(AdminSideUpsertLinkViewModel model);
        Task<Result> UpdateAsync(AdminSideUpsertLinkViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<List<ClientSideLinkViewModel>>> GetAllLinksForHeader();
        Task<Result<List<ClientSideLinkViewModel>>> GetAllLinksForFooter();
    }
}
