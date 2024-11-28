using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mediator;
using Kidemy.Application.Tools;
using Kidemy.Domain.Interfaces.SocialMedia;
using Kidemy.Domain.Models.SocialMedia;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.SocialMedia;
using Microsoft.AspNetCore.Http;
using Kidemy.Domain.Events.SocialMedia;

namespace Kidemy.Application.Services.Implementations
{
    public class SocialMediaService : ISocialMediaService
    {
        #region Fields

        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor

        public SocialMediaService(ISocialMediaRepository socialMediaRepository,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler)
        {
            _socialMediaRepository = socialMediaRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result<AdminSideUpsertSocialMediaViewModel>> GetSocialMediaById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertSocialMediaViewModel>(ErrorMessages.ProcessFailedError);

            var socialMedia = await _socialMediaRepository.GetByIdAsync(id);

            if (socialMedia is null) return Result.Failure<AdminSideUpsertSocialMediaViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertSocialMediaViewModel().MapFrom(socialMedia);

            await _localizingService.FillModelToEditByAdminAsync(socialMedia, model);

            return model;
        }

        public async Task<Result<FilterSocialMediaViewModel>> FilterSocialMediaAsync(FilterSocialMediaViewModel model)
        {
            if (model is null) return Result.Failure<FilterSocialMediaViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<SocialMedia>();

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                filterConditions.Add(s => s.Title.Contains(model.Title));
            }

            await _socialMediaRepository.FilterAsync(model, filterConditions, SocialMediaMapper.MapSocialMediaViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;
        }

        public async Task<Result> CreateSocialMediaAsync(AdminSideUpsertSocialMediaViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var socialMediaWithSameTitle = await _socialMediaRepository.FirstOrDefaultAsync(s => s.Title == model.Title);

            if (socialMediaWithSameTitle is not null)
            {
                return Result.Failure(ErrorMessages.DuplicatedSocialMediaTitleError);
            }

            if (model.Image is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            SocialMedia socialMedia = new()
            {
                Link = model.Link,
                Title = model.Title,
                Priority = model.Priority,
                ImageName = SaveSocialMediaImage(model.Image)
            };

            await _socialMediaRepository.InsertAsync(socialMedia);
            await _socialMediaRepository.SaveAsync();

            await _localizingService.SaveLocalizations(socialMedia, model);

            var socialMediaCreatedEvent = new SocialMediaCreateEvent(model.Id, model.Priority, model.Title, model.Link, model.ImageName);
            await _mediatorHandler.PublishEvent(socialMediaCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateSocialMediaAsync(AdminSideUpsertSocialMediaViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var socialMedia = await _socialMediaRepository.GetByIdAsync(model.Id);

            if (socialMedia is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var socialMediaCopy = socialMedia.DeepCopy<SocialMedia>();

            if (socialMedia!.Title != model.Title)
            {
                var socialMediaWithSameTitle = await _socialMediaRepository.FirstOrDefaultAsync(s => s.Title == model.Title && s.Id != socialMedia.Id);

                if (socialMediaWithSameTitle is not null)
                {
                    return Result.Failure(ErrorMessages.DuplicatedSocialMediaTitleError);
                }
            }

            if (model.Image is not null)
            {
                socialMedia.ImageName = SaveSocialMediaImage(model.Image!, socialMedia.ImageName);
            }

            if (string.IsNullOrEmpty(model.ImageName))
            {
                return Result.Failure(ErrorMessages.ImageIsNotValidError);
            }

            socialMedia.Link = model.Link;
            socialMedia.Title = model.Title;
            socialMedia.Priority = model.Priority;
            socialMedia.UpdatedDateOnUtc = DateTime.UtcNow;

            _socialMediaRepository.Update(socialMedia);
            await _socialMediaRepository.SaveAsync();

            await _localizingService.SaveLocalizations(socialMedia, model);

            var socialMediaUpdatedEvent = new SocialMediaUpdateEvent(socialMediaCopy.Id, model.Priority, model.Title, model.Link, model.ImageName,
                socialMediaCopy.Priority, socialMediaCopy.Title, socialMediaCopy.Link, socialMediaCopy.ImageName);
            await _mediatorHandler.PublishEvent(socialMediaUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> DeleteSocialMediaAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _socialMediaRepository.SoftDelete(id);
            await _socialMediaRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<List<ClientSideSocialMediaViewModel>>> GetAllSocialMedia()
        {
            var socialMedia = await _socialMediaRepository.GetAllAsync();

            var model = socialMedia.Select(s => new ClientSideSocialMediaViewModel().MapFrom(s)).ToList();

            return model;
        }

        #endregion

        #region Utilities

        private static string SaveSocialMediaImage(IFormFile socialMediaImage, string? lastImageName = null)
        {
            string imageName = string.Empty;

            if (socialMediaImage.IsImage())
            {
                imageName = Guid.NewGuid() + Path.GetExtension(socialMediaImage.FileName);

                if (lastImageName is not null)
                {
                    if (socialMediaImage.IsSVG())
                    {
                        socialMediaImage.AddImageToServerWithOutThumb(imageName, SiteTools.SocialMediaImagePath, 400, 300, lastImageName);
                    }
                    else
                    {
                        socialMediaImage.AddImageToServer(imageName, SiteTools.SocialMediaImagePath, 400, 300,
                            SiteTools.SocialMediaImageThumbPath, lastImageName);
                    }
                }
                else
                {
                    if (socialMediaImage.IsSVG())
                    {
                        socialMediaImage.AddImageToServerWithOutThumb(imageName, SiteTools.SocialMediaImagePath, 400, 300);
                    }
                    else
                    {
                        socialMediaImage.AddImageToServer(imageName, SiteTools.SocialMediaImagePath, 400, 300,
                            SiteTools.SocialMediaImageThumbPath);
                    }
                }
            }

            return imageName;
        }

        #endregion
    }
}