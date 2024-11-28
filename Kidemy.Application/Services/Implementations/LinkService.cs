using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Link;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Enums.Link;
using Kidemy.Domain.Events.Link;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Domain.Models.Link;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class LinkService : ILinkService
    {
        #region Fields

        private readonly ILinkRepository _linkRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public LinkService(ILinkRepository linkRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler)
        {
            _linkRepository = linkRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods
        public async Task<Result> CreateAsync(AdminSideUpsertLinkViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _linkRepository.AnyAsync(l => l.Title == model.Title))
            {
                return Result.Failure(ErrorMessages.DuplicatedTitleError);
            }

            Link link = new()
            {
                Title = model.Title,
                Address = model.Address,
                LinkType = model.LinkType,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            await _linkRepository.InsertAsync(link);
            await _linkRepository.SaveAsync();

            await _localizingService.SaveLocalizations(link, model);

            LinkCreatedEvent linkCreatedEvent = new(
                link.Title,
                link.Address,
                link.LinkType);

            await _mediatorHandler.PublishEvent(linkCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertLinkViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var link = await _linkRepository.GetByIdAsync(model.Id);

            if (link is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _linkRepository.AnyAsync(l => l.Title == model.Title && l.Id != model.Id))
            {
                return Result.Failure(ErrorMessages.DuplicatedTitleError);
            }

            var copiedLink = link?.DeepCopy<Link>();

            link.Title = model.Title;
            link.Address = model.Address;
            link.LinkType = model.LinkType;

            _linkRepository.Update(link);
            await _linkRepository.SaveAsync();

            LinkUpdatedEvent linkUpdatedEvent = new(
                link.Id,
                link.Title,
                model.Title,
                link.Address,
                model.Address,
                link.LinkType,
                model.LinkType);

            await _mediatorHandler.PublishEvent(linkUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _linkRepository.SoftDelete(id);
            await _linkRepository.SaveAsync();


            LinkDeletedEvent linkDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(linkDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result<FilterLinkViewModel>> FilterAsync(FilterLinkViewModel model)
        {
            if (model is null) return Result.Failure<FilterLinkViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Link>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            if (model.LinkType is not null)
            {
                filterConditions.Add(s => s.LinkType == model.LinkType);
            }

            await _linkRepository.FilterAsync(model, filterConditions, LinkMapper.MapLinkViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;

        }

        public async Task<Result<AdminSideUpsertLinkViewModel>> GetLinkForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertLinkViewModel>(ErrorMessages.ProcessFailedError);

            var link = await _linkRepository.GetByIdAsync(id);

            if (link is null) return Result.Failure<AdminSideUpsertLinkViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertLinkViewModel().MapFrom(link);

            await _localizingService.FillModelToEditByAdminAsync(link, model);

            return model;

        }

        public async Task<Result<List<ClientSideLinkViewModel>>> GetAllLinksForHeader()
        {

            var headerLinks = await _linkRepository.GetAllAsync(l => l.LinkType == Domain.Enums.Link.LinkType.Header);

            if (headerLinks is null || !headerLinks.Any())
            {
                return new List<ClientSideLinkViewModel>();
            }
            var models = headerLinks.Select(s => new ClientSideLinkViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        public async Task<Result<List<ClientSideLinkViewModel>>> GetAllLinksForFooter()
        {

            var footerLinks = await _linkRepository.GetAllAsync(l => l.LinkType == LinkType.Footer);

            if (footerLinks is null || !footerLinks.Any())
            {
                return new List<ClientSideLinkViewModel>();
            }

            var models = footerLinks.Select(s => new ClientSideLinkViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        } 

        #endregion
    }
}
