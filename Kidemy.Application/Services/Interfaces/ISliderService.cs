using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Enums.Slider;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISliderService
    {
        Task<Result> CreateSliderAsync(AdminSideCreateSliderViewModel model);
        Task<Result> UpdateSliderAsync(AdminSideUpdateSliderViewModel model);
        Task<Result<FilterSliderViewModel>> FilterAsync(FilterSliderViewModel model);
        Task<Result<AdminSideUpdateSliderViewModel>> GetSliderForEditById(int id);
        Task<Result> DeleteSliderAsync(int id);
        Task<List<ClientSideSliderViewModel>> GetAllSlidersByPlace(SliderPlace sliderPlace);

    }
}
