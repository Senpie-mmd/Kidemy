using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Enums.Slider;
using Kidemy.Domain.Events.Slider;
using Kidemy.Domain.Interfaces.Slider;
using Kidemy.Domain.Models.Slider;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region Fields

        private readonly ISliderRepository _sliderRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public SliderService(ISliderRepository sliderRepository,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler)
        {
            _sliderRepository = sliderRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods
        public async Task<Result<FilterSliderViewModel>> FilterAsync(FilterSliderViewModel model)
        {
            if (model is null) return Result.Failure<FilterSliderViewModel>(ErrorMessages.ProcessFailedError);

            var conditions = Filter.GenerateConditions<Slider>();

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                conditions.Add(s => s.Title.Contains(model.Title));
            }

            if (model.Priority is not null)
            {
                conditions.Add(s => s.Priority == model.Priority);
            }

            if (model.SliderPlace is not null)
            {
                conditions.Add(s => s.SliderPlace == model.SliderPlace);
            }

            if (model.IsPublished is not null)
            {
                conditions.Add(s => s.IsPublished == model.IsPublished);

            }
            await _sliderRepository.FilterAsync(model, conditions, SliderMapper.MapSliderViewModel);

            return model;

        }

        public async Task<Result> CreateSliderAsync(AdminSideCreateSliderViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.Image is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            Slider slider = new()
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                SliderPlace = model.SliderPlace,
                URL = model.URL,
                IsPublished = model.IsPublished,
                ImageName = SaveSliderImage(model.Image)
            };

            await _sliderRepository.InsertAsync(slider);
            await _sliderRepository.SaveAsync();

            await _localizingService.SaveLocalizations(slider, model);

            SliderCreatedEvent sliderCreatedEvent = new(
                slider.Id,
                slider.Title,
                slider.Description,
                slider.ImageName,
                slider.Priority,
                slider.SliderPlace,
                slider.URL,
                slider.IsPublished);

            await _mediatorHandler.PublishEvent(sliderCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateSliderAsync(AdminSideUpdateSliderViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var slider = await _sliderRepository.GetByIdAsync(model.Id);

            if (slider is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedSlider = slider?.DeepCopy<Slider>();

            if (model.Image is not null)
            {
                slider.ImageName = SaveSliderImage(model.Image!, slider.ImageName);
            }

            if (string.IsNullOrEmpty(model.ImageName))
            {
                return Result.Failure(ErrorMessages.ImageIsNotValidError);
            }

            slider.Title = model.Title;
            slider.Description = model.Description;
            slider.Priority = model.Priority;
            slider.SliderPlace = model.SliderPlace;
            slider.URL = model.URL;
            slider.IsPublished = model.IsPublished;
            slider.UpdatedDateOnUtc = DateTime.UtcNow;

            _sliderRepository.Update(slider);
            await _sliderRepository.SaveAsync();

            await _localizingService.SaveLocalizations(slider, model);

            SliderUpdatedEvent sliderUpdatedEvent = new(
                slider.Id,
                slider.Title,
                copiedSlider.Title,
                slider.Description,
                copiedSlider.Description,
                slider.ImageName,
                copiedSlider.ImageName,
                slider.Priority,
                copiedSlider.Priority,
                slider.SliderPlace,
                copiedSlider.SliderPlace,
                slider.URL,
                copiedSlider.URL,
                slider.IsPublished,
                copiedSlider.IsPublished);

            await _mediatorHandler.PublishEvent(sliderUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpdateSliderViewModel>> GetSliderForEditById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateSliderViewModel>(ErrorMessages.ProcessFailedError);

            var slider = await _sliderRepository.GetByIdAsync(id);

            if (slider is null) return Result.Failure<AdminSideUpdateSliderViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateSliderViewModel().MapFrom(slider);

            await _localizingService.FillModelToEditByAdminAsync(slider, model);

            return model;
        }

        public async Task<Result> DeleteSliderAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _sliderRepository.SoftDelete(id);
            await _sliderRepository.SaveAsync();

            var SliderDeletedEvent = new SliderDeletedEvent(id);
            await _mediatorHandler.PublishEvent(SliderDeletedEvent);

            return Result.Success();
        }

        private static string SaveSliderImage(IFormFile sliderImage, string? lastImageName = null)
        {
            string imageName = string.Empty;

            if (sliderImage.IsImage())
            {
                imageName = Guid.NewGuid() + Path.GetExtension(sliderImage.FileName);

                if (lastImageName is not null)
                {
                    if (Path.GetExtension(lastImageName).Contains(".svg"))
                    {
                        lastImageName.DeleteImage(SiteTools.SliderImagePath);
                    }
                    else
                    {
                        lastImageName.DeleteImage(SiteTools.SliderImagePath, SiteTools.SliderImageThumbPath);
                    }
                }
                if (sliderImage is not null)
                {
                    if (sliderImage.IsSVG())
                    {
                        sliderImage.AddImageToServerWithOutThumb(imageName, SiteTools.SliderImagePath, 400, 300);
                    }
                    else
                    {
                        sliderImage.AddImageToServer(imageName, SiteTools.SliderImagePath, 400, 300,
                            SiteTools.SliderImageThumbPath);
                    }
                }
            }

            return imageName;
        }

        public async Task<List<ClientSideSliderViewModel>> GetAllSlidersByPlace(SliderPlace sliderPlace)
        {

            var sliders = await _sliderRepository.GetAllAsync(s=>s.SliderPlace==sliderPlace);

            if (sliders is null || !sliders.Any())
            {
                return new List<ClientSideSliderViewModel>();
            }
            var models = sliders.Select(s => new ClientSideSliderViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        #endregion

    }
}
