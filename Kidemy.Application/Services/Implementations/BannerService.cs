using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Banner;
using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Events.Banner;
using Kidemy.Domain.Interfaces.Banner;
using Kidemy.Domain.Models.Banner;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace Kidemy.Application.Services.Implementations
{
    public class BannerService : IBannerService
    {
        #region Fields

        private readonly IBannerRepository _bannerRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public BannerService(IBannerRepository bannerRepository,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler)
        {
            _bannerRepository = bannerRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result<FilterBannerViewModel>> FilterAsync(FilterBannerViewModel model)
        {
            if (model is null) return Result.Failure<FilterBannerViewModel>(ErrorMessages.ProcessFailedError);

            var conditions = Filter.GenerateConditions<Banner>();

            if (model.Place is not null)
            {
                conditions.Add(s => s.Place == model.Place);
            }

            if (model.IsPublished is not null)
            {
                conditions.Add(s => s.IsPublished == model.IsPublished);

            }

            await _bannerRepository.FilterAsync(model, conditions, BannerMapper.MapBannerViewModel);

            return model;

        }

        public async Task<Result> CreateAsync(AdminSideCreateBannerViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _bannerRepository.AnyAsync(p => p.Place == model.Place))
            {
                return Result.Failure(ErrorMessages.ABannerIsAvailableForTheSelectedLocation);
            }

            if (model.Image is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            Banner banner = new()
            {
                Place = model.Place,
                URL = model.URL,
                IsPublished = model.IsPublished,
                ImageName = SaveBannerImage(model.Image)
            };

            await _bannerRepository.InsertAsync(banner);
            await _bannerRepository.SaveAsync();

            BannerCreatedEvent bannerCreatedEvent = new(
                banner.Id,
                banner.ImageName,
                banner.Place,
                banner.URL,
                banner.IsPublished);

            await _mediatorHandler.PublishEvent(bannerCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(AdminSideUpdateBannerViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _bannerRepository.AnyAsync(p => p.Place == model.Place && p.Id != model.Id))
            {
                return Result.Failure(ErrorMessages.ABannerIsAvailableForTheSelectedLocation);
            }

            var banner = await _bannerRepository.GetByIdAsync(model.Id);

            if (banner is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedBanner = banner?.DeepCopy<Banner>();

            if (model.Image is not null)
            {
                banner.ImageName = SaveBannerImage(model.Image!, banner.ImageName);
            }

            if (string.IsNullOrEmpty(model.ImageName))
            {
                return Result.Failure(ErrorMessages.ImageIsNotValidError);
            }

            banner.Place = model.Place;
            banner.URL = model.URL;
            banner.IsPublished = model.IsPublished;

            _bannerRepository.Update(banner);
            await _bannerRepository.SaveAsync();

            BannerUpdatedEvent bannerUpdatedEvent = new(
                banner.Id,
                banner.ImageName,
                copiedBanner.ImageName,
                banner.Place,
                copiedBanner.Place,
                banner.URL,
                copiedBanner.URL,
                banner.IsPublished,
                copiedBanner.IsPublished);

            await _mediatorHandler.PublishEvent(bannerUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpdateBannerViewModel>> GetBannerForEditById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateBannerViewModel>(ErrorMessages.ProcessFailedError);

            var banner = await _bannerRepository.GetByIdAsync(id);

            if (banner is null) return Result.Failure<AdminSideUpdateBannerViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateBannerViewModel().MapFrom(banner);

            return model;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _bannerRepository.SoftDelete(id);
            await _bannerRepository.SaveAsync();

            BannerDeletedEvent bannerDeletedEvent = new(id);
            await _mediatorHandler.PublishEvent(bannerDeletedEvent);

            return Result.Success();
        }

        public async Task<Result<ClientSideBannerViewModel>> GetBannerByPlaceForClientSide(BannerPlace bannerPlace)
        {

            var banner = await _bannerRepository.FirstOrDefaultAsync(b => b.Place == bannerPlace);

            if (banner is null) return Result.Failure<ClientSideBannerViewModel>(ErrorMessages.ProcessFailedError);

            var model = new ClientSideBannerViewModel().MapFrom(banner);

            return model;
        }
        #endregion

        #region Utilities

        private static string SaveBannerImage(IFormFile BannerImage, string? lastImageName = null)
        {
            string imageName = string.Empty;

            if (BannerImage.IsImage())
            {
                imageName = Guid.NewGuid() + Path.GetExtension(BannerImage.FileName);

                if (lastImageName is not null)
                {
                    if (Path.GetExtension(lastImageName).Contains(".svg"))
                    {
                        lastImageName.DeleteImage(SiteTools.BannerImagePath);
                    }
                    else
                    {
                        lastImageName.DeleteImage(SiteTools.BannerImagePath, SiteTools.BannerImageThumbPath);
                    }
                }
                if (BannerImage is not null)
                {
                    if (BannerImage.IsSVG())
                    {
                        BannerImage.AddImageToServerWithOutThumb(imageName, SiteTools.BannerImagePath, 400, 300);
                    }
                    else
                    {
                        BannerImage.AddImageToServer(imageName, SiteTools.BannerImagePath, 400, 300,
                            SiteTools.BannerImageThumbPath);
                    }
                }
            }

            return imageName;
        }

        #endregion

    }
}
