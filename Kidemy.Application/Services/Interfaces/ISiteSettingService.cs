using Kidemy.Application.ViewModels.SiteSetting;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISiteSettingService
    {
        Task<Result> UpdateSiteSettingAsync(AdminSideUpsertSiteSettingViewModel model);
        Task<Result<AdminSideUpsertSiteSettingViewModel>> GetSiteSettingForEditAsync();
        Task<Result<SiteSettingDetailsViewModel>> GetSiteSettingAsync();
    }
}
