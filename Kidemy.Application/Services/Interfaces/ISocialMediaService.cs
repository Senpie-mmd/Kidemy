using Kidemy.Application.ViewModels.SocialMedia;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISocialMediaService
    {
        Task<Result<AdminSideUpsertSocialMediaViewModel>> GetSocialMediaById(int id);

        Task<Result<FilterSocialMediaViewModel>> FilterSocialMediaAsync(FilterSocialMediaViewModel model);

        Task<Result> CreateSocialMediaAsync(AdminSideUpsertSocialMediaViewModel model);

        Task<Result> UpdateSocialMediaAsync(AdminSideUpsertSocialMediaViewModel model);

        Task<Result> DeleteSocialMediaAsync(int id);
        Task<Result<List<ClientSideSocialMediaViewModel>>> GetAllSocialMedia();
    }
}