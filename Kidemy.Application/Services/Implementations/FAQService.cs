using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.FAQ;
using Kidemy.Application.ViewModels.Link;
using Kidemy.Domain.Events.FAQ;
using Kidemy.Domain.Events.Link;
using Kidemy.Domain.Interfaces.FAQ;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Domain.Models.FAQ;
using Kidemy.Domain.Models.Link;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Implementations
{
    public class FAQService : IFAQService
    {
        #region Fields

        private readonly IFAQRepository _fAQRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public FAQService(IFAQRepository fAQRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler)
        {
            _fAQRepository = fAQRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods
        public async Task<Result> CreateAsync(AdminSideUpsertFAQViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _fAQRepository.AnyAsync(l => l.Title == model.Title))
            {
                return Result.Failure(ErrorMessages.DuplicatedTitleError);
            }

            FAQ fAQ = new()
            {
                Title = model.Title,
                Answer = model.Answer,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            await _fAQRepository.InsertAsync(fAQ);
            await _fAQRepository.SaveAsync();

            await _localizingService.SaveLocalizations(fAQ, model);

            FAQCreatedEvent fAQCreatedEvent = new(
                fAQ.Title,
                fAQ.Answer);

            await _mediatorHandler.PublishEvent(fAQCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _fAQRepository.SoftDelete(id);
            await _fAQRepository.SaveAsync();


            FAQDeletedEvent fAQDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(fAQDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<FilterFAQViewModel>> FilterAsync(FilterFAQViewModel model)
        {
            if (model is null) return Result.Failure<FilterFAQViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<FAQ>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _fAQRepository.FilterAsync(model, filterConditions, FAQMapper.MapFAQViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;
        }

        public async Task<Result<AdminSideUpsertFAQViewModel>> GetFAQForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertFAQViewModel>(ErrorMessages.ProcessFailedError);

            var fAQ = await _fAQRepository.GetByIdAsync(id);

            if (fAQ is null) return Result.Failure<AdminSideUpsertFAQViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertFAQViewModel().MapFrom(fAQ);

            await _localizingService.FillModelToEditByAdminAsync(fAQ, model);

            return model;
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertFAQViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var fAQ = await _fAQRepository.GetByIdAsync(model.Id);

            if (fAQ is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (await _fAQRepository.AnyAsync(f => f.Title == model.Title && f.Id != model.Id))
            {
                return Result.Failure(ErrorMessages.DuplicatedTitleError);
            }

            var copiedFAQ = fAQ?.DeepCopy<FAQ>();

            fAQ.Title = model.Title;
            fAQ.Answer = model.Answer;


            _fAQRepository.Update(fAQ);
            await _fAQRepository.SaveAsync();

            FAQUpdatedEvent fAQUpdatedEvent = new(
                model.Id,
                copiedFAQ.Title,
                model.Title,
                copiedFAQ.Answer,
                model.Answer);

            await _mediatorHandler.PublishEvent(fAQUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<List<ClientSideFAQViewModel>>> GetAllFAQs()
        {

            var fAQs = await _fAQRepository.GetAllAsync();

            if (fAQs is null || !fAQs.Any())
            {
                return new List<ClientSideFAQViewModel>();
            }

            var models = fAQs.Select(s => new ClientSideFAQViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        } 

        #endregion
    }
}
