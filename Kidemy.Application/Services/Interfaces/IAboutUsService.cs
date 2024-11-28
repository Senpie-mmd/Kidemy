using Kidemy.Application.ViewModels.AboutUs;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<Result> UpdateAboutUsAsync(AdminSideUpsertAboutUsViewModel model);

        Task<Result<AdminSideUpsertAboutUsViewModel>> GetAboutUsAsync();

        Task<Result> CreateAboutUsProgressBar(CreateAboutUsProgressBarViewModel model);

        Task<Result<FilterProgressBarViewModel>> FilterProgressBarViewModel(FilterProgressBarViewModel filter);

        Task<Result<List<ProgressBarListViewModel>>> GetAllProgressBarList();

        Task<Result> DeleteProgressBar(int id);

        Task<Result<UpdateAboutUsProgressBarViewModel>> GetProgressBar(int id);

        Task<Result> UpdateProgressBar(UpdateAboutUsProgressBarViewModel model);

        Task<Result<ClientSideAboutUsPageViewModel>> GetAboutUsPageForUser();
    }
}
