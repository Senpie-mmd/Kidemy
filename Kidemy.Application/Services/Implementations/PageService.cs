using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Events.Page;
using Kidemy.Domain.Interfaces.Page;
using Kidemy.Domain.Models.Page;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class PageService : IPageService
    {
        #region Fields

        private readonly IPageRepository _pageRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public PageService(IPageRepository pageRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler)
        {
            _pageRepository = pageRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }
       
        #endregion

        #region Methods
        public async Task<Result> CreateAsync(AdminSideUpsertPageViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            string slug = model.Slug.Replace(" ", "-");

            if (await _pageRepository.AnyAsync(p => p.Title == model.Title))
            {
                return Result.Failure(ErrorMessages.DuplicateTitle);
            }

            if (await _pageRepository.AnyAsync(p => p.Slug == model.Slug))
            {
                return Result.Failure(ErrorMessages.DuplicateSlug);
            }

            Page page = new()
            {
                Title = model.Title,
                Body = model.Body,
                Slug = slug,
                IsPublished = model.IsPublished,
                LinkPlace = model.LinkPlace,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            await _pageRepository.InsertAsync(page);
            await _pageRepository.SaveAsync();

            await _localizingService.SaveLocalizations(page, model);

            PageCreatedEvent pageCreatedEvent = new(
                page.Title,
                page.Body,
                page.Slug,
                page.IsPublished,
                page.LinkPlace);

            await _mediatorHandler.PublishEvent(pageCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertPageViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            string slug = model.Slug.Replace(" ", "-");

            if (await _pageRepository.AnyAsync(p => p.Title == model.Title && p.Id != model.Id))
            {
                return Result.Failure(ErrorMessages.DuplicateTitle);
            }

            if (await _pageRepository.AnyAsync(p => p.Slug == model.Slug && p.Id != model.Id))
            {
                return Result.Failure(ErrorMessages.DuplicateSlug);
            }

            var page = await _pageRepository.GetByIdAsync(model.Id);

            if (page is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedPage = page?.DeepCopy<Page>();

            page.Title = model.Title;
            page.Body = model.Body;
            page.Slug = slug;
            page.IsPublished = model.IsPublished;
            page.LinkPlace = model.LinkPlace;

            _pageRepository.Update(page);
            await _pageRepository.SaveAsync();

            PageUpdatedEvent pageUpdatedEvent = new(
                page.Id,
                page.Title,
                copiedPage.Title,
                page.Body,
                copiedPage.Body,
                page.Slug,
                copiedPage.Slug,
                page.IsPublished,
                copiedPage.IsPublished,
                page.LinkPlace,
                copiedPage.LinkPlace);


            await _mediatorHandler.PublishEvent(pageUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _pageRepository.SoftDelete(id);
            await _pageRepository.SaveAsync();


            PageDeletedEvent pageDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(pageDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result<FilterPageViewModel>> FilterAsync(FilterPageViewModel model)
        {
            if (model is null) return Result.Failure<FilterPageViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Page>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _pageRepository.FilterAsync(model, filterConditions, PageMapper.MapPageViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;

        }

        public async Task<Result<AdminSideUpsertPageViewModel>> GetPageForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertPageViewModel>(ErrorMessages.ProcessFailedError);

            var page = await _pageRepository.GetByIdAsync(id);

            if (page is null) return Result.Failure<AdminSideUpsertPageViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertPageViewModel().MapFrom(page);

            await _localizingService.FillModelToEditByAdminAsync(page, model);

            return model;

        }

        public async Task<Result<ClientSidePageViewModel>> GetPageForUserBySlug(string slug)
        {
            if (slug is null)
                return Result.Failure<ClientSidePageViewModel>(ErrorMessages.FailedOperationError);

            var page = await _pageRepository.FirstOrDefaultAsync(n => n.Slug == slug);

            if (page is null)
                return Result.Failure<ClientSidePageViewModel>(ErrorMessages.FailedOperationError);

            var model = new ClientSidePageViewModel().MapFrom(page);

            return model;
        }

        public async Task<Result<List<ClientSidePageViewModel>>> GetAllPagesForHeader()
        {

            var headerPages = await _pageRepository.GetAllAsync(p => p.IsPublished == true && p.LinkPlace == Domain.Enums.Page.LinkPlace.Header);

            if (headerPages is null || !headerPages.Any())
            {
                return new List<ClientSidePageViewModel>();
            }
            var models = headerPages.Select(s => new ClientSidePageViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        public async Task<Result<List<ClientSidePageViewModel>>> GetAllPagesForFooter()
        {

            var footerPages = await _pageRepository.GetAllAsync(p => p.IsPublished == true && p.LinkPlace == Domain.Enums.Page.LinkPlace.Footer);

            if (footerPages is null || !footerPages.Any())
            {
                return new List<ClientSidePageViewModel>();
            }
            var models = footerPages.Select(s => new ClientSidePageViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        } 

        #endregion
    }
}
