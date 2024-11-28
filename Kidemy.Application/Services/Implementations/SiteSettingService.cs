using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.SiteSetting;
using Kidemy.Domain.Interfaces.SiteSetting;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class SiteSettingService : ISiteSettingService
    {
        #region Fields

        private readonly ISiteSettingRepository _siteSettingRepository;
        private readonly ISmsProviderService _smsProviderService;
        private readonly ILogger<SiteSettingService> _logger;
        private readonly ILocalizingService _localizingService;

        #endregion

        #region Constructor
        public SiteSettingService(ISiteSettingRepository siteSettingRepository, ILogger<SiteSettingService> logger, ILocalizingService localizingService,
            ISmsProviderService smsProviderService)

        {
            _siteSettingRepository = siteSettingRepository;
            _logger = logger;
            _localizingService = localizingService;
            _smsProviderService = smsProviderService;
        }

        #endregion

        #region Methods
        public async Task<Result> UpdateSiteSettingAsync(AdminSideUpsertSiteSettingViewModel model)
        {
            var defaultSmsProvider = await _smsProviderService.GetDefaultSmsProvider();

            if (defaultSmsProvider != null)
            {
                if (defaultSmsProvider.SmsProviderType != model.SmsProviderType)
                {
                    defaultSmsProvider.IsDefault = false;
                    await _smsProviderService.UpdateSmsProviderAsync(defaultSmsProvider);

                    var smsProvider = await _smsProviderService.GetSmsProviderType(model.SmsProviderType);
                    smsProvider.IsDefault = true;
                    await _smsProviderService.UpdateSmsProviderAsync(smsProvider);
                }
            }
            else
            {
                var smsProvider = await _smsProviderService.GetSmsProviderType(model.SmsProviderType);
                smsProvider.IsDefault = true;
                await _smsProviderService.UpdateSmsProviderAsync(smsProvider);
            }

            if (model == null) return Result.Failure<AdminSideUpsertSiteSettingViewModel>(ErrorMessages.ProcessFailedError);

            if (model.LogoName is null && model.LogoImage is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            var siteSetting = await _siteSettingRepository.FirstOrDefaultAsync();

            if (siteSetting == null)
            {
                _logger.LogError($"Ther is no aboutUs");
                return Result.Failure<AdminSideUpsertSiteSettingViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.LogoImage != null)
            {
                siteSetting.LogoName = SaveSiteSettingImage(model.LogoImage, siteSetting.LogoName);
            }

            siteSetting.Email = model.Email;
            siteSetting.Mobile = model.Mobile;
            siteSetting.Mobile2 = model.Mobile2;
            siteSetting.Address = model.Address;
            siteSetting.CollectionManagement = model.CollectionManagement;
            //siteSetting.FooterDescription = model.FooterDescription;
            siteSetting.CopyrightDescription = model.CopyrightDescription;
            siteSetting.NewsletterDescription = model.NewsletterDescription;
            siteSetting.WithdrawRequestMinimumAmount = model.WithdrawRequestMinimumAmount.Value;

            _siteSettingRepository.Update(siteSetting);
            await _siteSettingRepository.SaveAsync();

            await _localizingService.SaveLocalizations(siteSetting, model);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpsertSiteSettingViewModel>> GetSiteSettingForEditAsync()
        {

            var siteSetting = await _siteSettingRepository.FirstOrDefaultAsync();

            if (siteSetting is null) return Result.Failure<AdminSideUpsertSiteSettingViewModel>(ErrorMessages.ProcessFailedError);

            var defaultSmsProvider = await _smsProviderService.GetDefaultSmsProvider();
            var model = new AdminSideUpsertSiteSettingViewModel().MapFrom(siteSetting);
            model.SmsProviderType = defaultSmsProvider.SmsProviderType;

            await _localizingService.FillModelToEditByAdminAsync(siteSetting, model);

            return model;
        }

        public async Task<Result<SiteSettingDetailsViewModel>> GetSiteSettingAsync()
        {
            var siteSetting = await _siteSettingRepository.FirstOrDefaultAsync();

            var model = new SiteSettingDetailsViewModel().MapFrom(siteSetting);

            await _localizingService.TranslateModelAsync(model);

            return model;
        }

        private static string SaveSiteSettingImage(IFormFile siteSettingImage, string? lastImageName = null)
        {
            string imageName = string.Empty;

            if (siteSettingImage.IsImage())
            {
                imageName = Guid.NewGuid() + Path.GetExtension(siteSettingImage.FileName);

                if (lastImageName is not null)
                {
                    if (Path.GetExtension(lastImageName).Contains(".svg"))
                    {
                        lastImageName.DeleteImage(SiteTools.SiteSettingImagePath);
                    }
                    else
                    {
                        lastImageName.DeleteImage(SiteTools.SiteSettingImagePath, SiteTools.SiteSettingImageThumbPath);

                    }
                }

                if (siteSettingImage is not null)
                {
                    if (siteSettingImage.IsSVG())
                    {
                        siteSettingImage.AddImageToServerWithOutThumb(imageName, SiteTools.SiteSettingImagePath, 400, 300);
                    }
                    else
                    {
                        siteSettingImage.AddImageToServer(imageName, SiteTools.SiteSettingImagePath, 400, 300,
                            SiteTools.SiteSettingImageThumbPath);
                    }
                }
            }

            return imageName;
        }

        #endregion
    }
}
