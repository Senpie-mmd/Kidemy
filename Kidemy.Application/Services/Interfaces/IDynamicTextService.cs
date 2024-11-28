using Kidemy.Application.ViewModels.DynamicText;
using Kidemy.Domain.Enums.DynamicText;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IDynamicTextService
    {
        Task<Result<ClientSideDynamicTextViewModel>> GetDynamicTextByPosition(DynamicTextPosition model);
        Task<Result<FilterDynamicTextViewModel>> FilterAsync(FilterDynamicTextViewModel model);
        Task<Result> UpdateAsync(AdminSideUpdateDynamicTextViewModel model);
        Task<Result<AdminSideUpdateDynamicTextViewModel>> GetDynamicTextForEditByIdAsync(int id);
        Task<Result<List<ClientSideDynamicTextViewModel>>> GetMainBannerText();

    }
}
