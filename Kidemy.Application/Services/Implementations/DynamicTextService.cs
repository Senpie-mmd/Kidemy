using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.DynamicText;
using Kidemy.Domain.Enums.DynamicText;
using Kidemy.Domain.Events.DynamicText;
using Kidemy.Domain.Interfaces.DynamicText;
using Kidemy.Domain.Models.DynamicText;
using Kidemy.Domain.Models.Slider;
using Kidemy.Domain.Shared;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Kidemy.Application.Services.Implementations
{
    public class DynamicTextService : IDynamicTextService
    {
        #region Fields

        private readonly IDynamicTextRepository _dynamicTextRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public DynamicTextService(IDynamicTextRepository dynamicTextRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler)
        {
            _dynamicTextRepository = dynamicTextRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result<FilterDynamicTextViewModel>> FilterAsync(FilterDynamicTextViewModel model)
        {
            if (model is null) return Result.Failure<FilterDynamicTextViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<DynamicText>();

            if (model.Position is not null)
            {
                filterConditions.Add(s => s.Position == model.Position);
            }

            await _dynamicTextRepository.FilterAsync(model, filterConditions, DynamicTextMapper.MapDynamicTextViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;

        }

        public async Task<Result<List<ClientSideDynamicTextViewModel>>> GetMainBannerText()
        {
            var positions = new List<DynamicTextPosition>
            {
                DynamicTextPosition.TitleOfTopOfHomePage,
                DynamicTextPosition.DescriptionOfTopOfHomePage,
                DynamicTextPosition.TheFirstFeature,
                DynamicTextPosition.TheSecondFeature,
                DynamicTextPosition.TheThirdFeature,
            };

            var result = await _dynamicTextRepository.GetAllAsync(x => positions.Contains(x.Position));

            if (result is null || !result.Any())
            {
                return Result.Failure<List<ClientSideDynamicTextViewModel>>(ErrorMessages.ItemNotFoundError);
            }

            var model = result.Select(x => new ClientSideDynamicTextViewModel().MapFrom(x)).ToList();

            return model;
        }

        public async Task<Result<ClientSideDynamicTextViewModel>> GetDynamicTextByPosition(DynamicTextPosition position)
        {

            var dynamicText = await _dynamicTextRepository.FirstOrDefaultAsync(d => d.Position == position);

            if (dynamicText is null) return Result.Failure<ClientSideDynamicTextViewModel>(ErrorMessages.ProcessFailedError);

            var model = new ClientSideDynamicTextViewModel().MapFrom(dynamicText);

            await _localizingService.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result<AdminSideUpdateDynamicTextViewModel>> GetDynamicTextForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateDynamicTextViewModel>(ErrorMessages.ProcessFailedError);

            var dynamicText = await _dynamicTextRepository.GetByIdAsync(id);

            if (dynamicText is null) return Result.Failure<AdminSideUpdateDynamicTextViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateDynamicTextViewModel().MapFrom(dynamicText);

            return model;

        }

        public async Task<Result> UpdateAsync(AdminSideUpdateDynamicTextViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var dynamicText = await _dynamicTextRepository.GetByIdAsync(model.Id);

            if (dynamicText is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedDynamicText = dynamicText?.DeepCopy<DynamicText>();

            dynamicText.Text = model.Text;
            dynamicText.Position = model.Position;

            _dynamicTextRepository.Update(dynamicText);
            await _dynamicTextRepository.SaveAsync();


            await _localizingService.SaveLocalizations(dynamicText, model);

            DynamicTextUpdatedEvent dynamicTextUpdatedEvent = new(
                dynamicText.Id,
                dynamicText.Text,
                copiedDynamicText.Text,
                dynamicText.Position,
                copiedDynamicText.Position);


            await _mediatorHandler.PublishEvent(dynamicTextUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }


        #endregion

    }
}
