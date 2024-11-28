using Kidemy.Application.ViewModels.Banner;
using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IBannerService
    {
        Task<Result<FilterBannerViewModel>> FilterAsync(FilterBannerViewModel model);
        Task<Result<AdminSideUpdateBannerViewModel>> GetBannerForEditById(int id);
        Task<Result> CreateAsync(AdminSideCreateBannerViewModel model);
        Task<Result> UpdateAsync(AdminSideUpdateBannerViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result<ClientSideBannerViewModel>> GetBannerByPlaceForClientSide(BannerPlace bannerPlace);
    }
}
