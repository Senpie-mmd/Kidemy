using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.AboutUs;
using Kidemy.Domain.Events.AboutUs;
using Kidemy.Domain.Interfaces.AboutUs;
using Kidemy.Domain.Models.AboutUs;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class AboutUsService : IAboutUsService
    {
        #region Fields

        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ILogger<AboutUsService> _logger;
        private readonly ILocalizingService _localizing;
        private readonly IAboutUsProgressBarRepository _progressBarRepository;

        #endregion

        #region Constructor
        public AboutUsService(IAboutUsRepository aboutUsRepository,
            IMediatorHandler mediatorHandler,
            ILogger<AboutUsService> logger,
            ILocalizingService localizing,
            IAboutUsProgressBarRepository progressBarRepository)

        {
            _aboutUsRepository = aboutUsRepository;
            _mediatorHandler = mediatorHandler;
            _logger = logger;
            _localizing = localizing;
            _progressBarRepository = progressBarRepository;
        }
        #endregion

        #region Methods
        public async Task<Result> UpdateAboutUsAsync(AdminSideUpsertAboutUsViewModel model)
        {
            if (model == null) return Result.Failure<AdminSideUpsertAboutUsViewModel>(ErrorMessages.ProcessFailedError);

            var aboutUs = await _aboutUsRepository.FirstOrDefaultAsync();

            var copiedAboutUs = aboutUs?.DeepCopy<AboutUs>();

            if (aboutUs == null)
            {
                _logger.LogError($"There is no aboutUs");
                return Result.Failure<AdminSideUpsertAboutUsViewModel>(ErrorMessages.ProcessFailedError);
            }

            aboutUs.Title = model.Title;
            aboutUs.Description = model.Description;
            aboutUs.OurGoal = model.OurGoal;
            aboutUs.OurGoalTitle = model.OurGoalTitle;
            aboutUs.OurGoalDescription = model.OurGoalDescription;
            aboutUs.PageTitle = model.PageTitle;
            aboutUs.OurGoalFeatures = model.OurGoalFeatures;

            if (model.ImageFileNumber1 is not null)
            {
                aboutUs.ImageNumber1 = SiteTools.AboutUsImagePath + AddAboutUsImageToServer(model.ImageFileNumber1);
            }

            if (model.ImageFileNumber2 is not null)
            {
                aboutUs.ImageNumber2 = SiteTools.AboutUsImagePath + AddAboutUsImageToServer(model.ImageFileNumber2);
            }

            if (model.ImageFileNumber3 is not null)
            {
                aboutUs.ImageNumber3 = SiteTools.AboutUsImagePath + AddAboutUsImageToServer(model.ImageFileNumber3);
            }

            if (model.ImageFileNumber4 is not null)
            {
                aboutUs.ImageNumber4 = SiteTools.AboutUsImagePath + AddAboutUsImageToServer(model.ImageFileNumber4);
            }

            if (model.ImageFileNumber5 is not null)
            {
                aboutUs.ImageNumber5 = SiteTools.AboutUsImagePath + AddAboutUsImageToServer(model.ImageFileNumber5);
            }

            _aboutUsRepository.Update(aboutUs);
            await _aboutUsRepository.SaveAsync();

            await _localizing.SaveLocalizations(aboutUs, model);

            var aboutUsUpdatedEvent = new AboutUsUpdatedEvent(
                aboutUs.Id,
                copiedAboutUs.PageTitle,
                copiedAboutUs.Title,
                copiedAboutUs.Description,
                copiedAboutUs.OurGoal,
                copiedAboutUs.OurGoalTitle,
                copiedAboutUs.OurGoalDescription,
                copiedAboutUs.OurGoalFeatures,
                model.PageTitle,
                model.Title,
                model.Description,
                model.OurGoal,
                model.OurGoalTitle,
                model.OurGoalDescription,
                model.OurGoalFeatures
                );

            await _mediatorHandler.PublishEvent(aboutUsUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpsertAboutUsViewModel>> GetAboutUsAsync()
        {

            var aboutUs = await _aboutUsRepository.FirstOrDefaultAsync();

            if (aboutUs is null) return Result.Failure<AdminSideUpsertAboutUsViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertAboutUsViewModel().MapFrom(aboutUs);

            await _localizing.FillModelToEditByAdminAsync(aboutUs, model);

            return model;
        }

        public async Task<Result> CreateAboutUsProgressBar(CreateAboutUsProgressBarViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            AboutUsProgressBar progressBar = new AboutUsProgressBar()
            {
                Percent = model.Percent,
                Title = model.Title
            };

            await _progressBarRepository.InsertAsync(progressBar);
            await _progressBarRepository.SaveAsync();

            await _localizing.SaveLocalizations(progressBar, model);

            var AboutUsProgressBarCreatedEvent = new AboutUsProgressBarCreatedEvent(model.Title, model.Percent);

            await _mediatorHandler.PublishEvent(AboutUsProgressBarCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<FilterProgressBarViewModel>> FilterProgressBarViewModel(FilterProgressBarViewModel filter)
        {
            if (filter is null) return Result.Failure<FilterProgressBarViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<AboutUsProgressBar>();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(n => n.Title.Contains(filter.Title));
            }

            if (filter.Percent is not null)
            {
                conditions.Add(n => n.Percent >= filter.Percent);
            }

            await _progressBarRepository.FilterAsync(filter, conditions, AboutUsMapper.MapAdminSideTicketViewModel);
            await _localizing.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result<List<ProgressBarListViewModel>>> GetAllProgressBarList()
        {
            var items = await _progressBarRepository.GetAllAsync();
            if (items is null) return Result.Failure<List<ProgressBarListViewModel>>(ErrorMessages.FailedOperationError);

            List<ProgressBarListViewModel> model = new List<ProgressBarListViewModel>();
            model.AddRange(items.Select(n => new ProgressBarListViewModel().MapFrom(n)));

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result> DeleteProgressBar(int id)
        {
            if (id < 1) return Result.Failure(ErrorMessages.FailedOperationError);
            var progressBar = await _progressBarRepository.GetByIdAsync(id);

            if (progressBar is null) return Result.Failure(ErrorMessages.FailedOperationError);

            _progressBarRepository.Delete(progressBar);
            await _progressBarRepository.SaveAsync();

            AboutUsProgressBarDeletedEvent AboutUsProgressBarDeletedEvent = new AboutUsProgressBarDeletedEvent(id);

            await _mediatorHandler.PublishEvent(AboutUsProgressBarDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<UpdateAboutUsProgressBarViewModel>> GetProgressBar(int id)
        {
            if (id is < 1) return Result.Failure<UpdateAboutUsProgressBarViewModel>(ErrorMessages.FailedOperationError);

            var progressBar = await _progressBarRepository.GetByIdAsync(id);
            if (progressBar is null) return Result.Failure<UpdateAboutUsProgressBarViewModel>(ErrorMessages.FailedOperationError);

            UpdateAboutUsProgressBarViewModel model = new UpdateAboutUsProgressBarViewModel().MapFrom(progressBar);
            await _localizing.FillModelToEditByAdminAsync(progressBar, model);

            return model;
        }

        public async Task<Result> UpdateProgressBar(UpdateAboutUsProgressBarViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var progressBar = await _progressBarRepository.GetByIdAsync(model.Id);
            var copy = progressBar.DeepCopy<AboutUsProgressBar>();
            if (progressBar is null) return Result.Failure(ErrorMessages.FailedOperationError);

            progressBar.Title = model.Title;
            progressBar.Percent = model.Percent;

            _progressBarRepository.Update(progressBar);
            await _progressBarRepository.SaveAsync();

            await _localizing.SaveLocalizations(progressBar, model);

            AboutUsProgressBarUpdatedEvent AboutUsProgressBarUpdatedEvent = new AboutUsProgressBarUpdatedEvent(copy.Id,
                copy.Title,
                copy.Percent,
                model.Title,
                model.Percent);

            await _mediatorHandler.PublishEvent(AboutUsProgressBarUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<ClientSideAboutUsPageViewModel>> GetAboutUsPageForUser()
        {
            var item = await _aboutUsRepository.FirstOrDefaultAsync();
            if (item is null) return Result.Failure<ClientSideAboutUsPageViewModel>(ErrorMessages.FailedOperationError);

            ClientSideAboutUsPageViewModel model = new ClientSideAboutUsPageViewModel().MapFrom(item);

            await _localizing.TranslateModelAsync(model);

            return model;
        }
        #endregion

        #region Utilities
        private static string AddAboutUsImageToServer(IFormFile imageFile)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            imageFile.AddImageToServerWithOutThumb(imageName, SiteTools.AboutUsImagePath, null, null, null, true);

            return imageName;
        }
        #endregion
    }
}
