using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SmsProvider;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Interfaces.SmsProvider;
using Kidemy.Domain.Models.SmsProvider;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class SmsProviderService : ISmsProviderService
    {
        #region Fields

        private readonly ISmsProviderRepository _smsProviderRepository;
        private readonly ILogger<SmsProviderService> _logger;

        #endregion

        #region Constructor
        public SmsProviderService(ISmsProviderRepository smsProviderRepository, ILogger<SmsProviderService> logger)

        {
            _smsProviderRepository = smsProviderRepository;
            _logger = logger;
        }
        #endregion

        #region Methods

        public async Task<Result> UpdateSmsProviderAsync(AdminSideUpsertSmsProviderViewModel model)
        {
            if (model == null) return Result.Failure<AdminSideUpsertSmsProviderViewModel>(ErrorMessages.ProcessFailedError);

            var smsProvider = await _smsProviderRepository.GetByIdAsync(model.Id);

            if (!model.IsDefault && smsProvider.IsDefault)
            {
                return Result.Failure<AdminSideUpsertSmsProviderViewModel>(ErrorMessages.OneDefaultOptionMustBePresent);
            }

            if (model.IsDefault == true)
            {
                var defaultSmsProvider = await _smsProviderRepository.GetDefaultSmsProvider();
                if (defaultSmsProvider != null)
                {
                    var id = defaultSmsProvider.Id;
                    if (id != model.Id)
                    {
                        defaultSmsProvider.IsDefault = false;
                        _smsProviderRepository.Update(defaultSmsProvider);
                    }
                }

            }

            if (smsProvider == null)
            {
                _logger.LogError($"Ther is no aboutUs");
                return Result.Failure<AdminSideUpsertSmsProviderViewModel>(ErrorMessages.ProcessFailedError);
            }

            smsProvider.ApiKey = model.ApiKey;
            smsProvider.IsDefault = model.IsDefault;

            _smsProviderRepository.Update(smsProvider);
            await _smsProviderRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<AdminSideUpsertSmsProviderViewModel>> GetSmsProviderForEditAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertSmsProviderViewModel>(ErrorMessages.ProcessFailedError);

            var smsProvider = await _smsProviderRepository.GetByIdAsync(id);

            if (smsProvider is null) return Result.Failure<AdminSideUpsertSmsProviderViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertSmsProviderViewModel().MapFrom(smsProvider);


            return model;
        }

        public async Task<SmsProviderDetailsViewModel> GetSmsProviderAsync()
        {
            var siteSetting = await _smsProviderRepository.FirstOrDefaultAsync();

            var model = new SmsProviderDetailsViewModel().MapFrom(siteSetting);

            return model;
        }

        public async Task<Result<FilterSmsProviderViewModel>> FilterAsync(FilterSmsProviderViewModel model)
        {
            if (model is null) return Result.Failure<FilterSmsProviderViewModel>(ErrorMessages.ProcessFailedError);

            var conditions = Filter.GenerateConditions<SmsProvider>();

            if (model.IsDefault.HasValue)
            {
                conditions.Add(s => s.IsDefault == model.IsDefault);
            }

            await _smsProviderRepository.FilterAsync(model, conditions, SmsProviderMapper.MapSmsProviderViewModel);

            return model;

        }

        public async Task<AdminSideUpsertSmsProviderViewModel> GetDefaultSmsProvider()
        {

            var smsProvider = await _smsProviderRepository.GetDefaultSmsProvider();

            if (smsProvider is null) return new AdminSideUpsertSmsProviderViewModel();

            var model = new AdminSideUpsertSmsProviderViewModel().MapFrom(smsProvider);


            return model;
        }

        public async Task<AdminSideUpsertSmsProviderViewModel> GetSmsProviderType(SmsProviderType smsProviderType)
        {
            var smsProvider = await _smsProviderRepository.GetSmsProviderType(smsProviderType);

            if (smsProvider is null) return new AdminSideUpsertSmsProviderViewModel();

            var model = new AdminSideUpsertSmsProviderViewModel().MapFrom(smsProvider);


            return model;
        }

        public async Task<Result<SmsProviderType>> GetDefaultSmsProviderType()
        {
            var smsProviderType = await _smsProviderRepository.GetDefaultSmsProviderType();

            return smsProviderType;
        }

        #endregion
    }
}
