using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.MasterContract;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Events.Master;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class MasterContractService : IMasterContractService
    {
        #region Fields

        private readonly IMasterContractRepository _masterContractRepository;
        private readonly IUploadedMasterContractService _uploadedMasterContractService;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public MasterContractService(IMasterContractRepository masterContractRepository, ILocalizingService localizingService, IMediatorHandler mediatorHandler, IUploadedMasterContractService uploadedMasterContractService)
        {
            _masterContractRepository = masterContractRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
            _uploadedMasterContractService = uploadedMasterContractService;
        }

        #endregion

        #region Actions
        public async Task<Result> CreateAsync(AdminSideCreateMasterContractViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            MasterContract masterContract = new()
            {
                Title = model.Title,
                FileName = model.FileName,
                IsPublished = model.IsPublished,
                CreatedDateOnUtc = DateTime.UtcNow
            };

            await _masterContractRepository.InsertAsync(masterContract);
            await _masterContractRepository.SaveAsync();

            await _localizingService.SaveLocalizations(masterContract, model);

            MasterContractCreatedEvent masterContractCreatedEvent = new(
                masterContract.Id,
                masterContract.Title,
                masterContract.FileName,
                masterContract.IsPublished);

            await _mediatorHandler.PublishEvent(masterContractCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(AdminSideUpdateMasterContractViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var masterContract = await _masterContractRepository.GetByIdAsync(model.Id);

            if (masterContract is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var copiedMasterContract = masterContract?.DeepCopy<MasterContract>();

            masterContract.Title = model.Title;
            masterContract.FileName = model.FileName;
            masterContract.IsPublished = model.IsPublished;

            _masterContractRepository.Update(masterContract);
            await _masterContractRepository.SaveAsync();

            MasterContractUpdatedEvent masterContractUpdatedEvent = new(
                masterContract.Id,
                masterContract.Title,
                copiedMasterContract.Title,
                masterContract.FileName,
                copiedMasterContract.FileName,
                masterContract.IsPublished,
                copiedMasterContract.IsPublished);

            await _mediatorHandler.PublishEvent(masterContractUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.FailedOperationError);

            await _masterContractRepository.SoftDelete(id);
            await _masterContractRepository.SaveAsync();


            MasterContractDeletedEvent masterContractDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(masterContractDeletedEvent);

            return Result.Success();
        }

        public async Task<Result<FilterMasterContractViewModel>> FilterAsync(FilterMasterContractViewModel model)
        {
            if (model is null) return Result.Failure<FilterMasterContractViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<MasterContract>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _masterContractRepository.FilterAsync(model, filterConditions, MasterContractMapper.MapMasterContractViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;
        }

        public async Task<Result<AdminSideUpdateMasterContractViewModel>> GetMasterContractForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpdateMasterContractViewModel>(ErrorMessages.ProcessFailedError);

            var masterContract = await _masterContractRepository.GetByIdAsync(id);

            if (masterContract is null) return Result.Failure<AdminSideUpdateMasterContractViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpdateMasterContractViewModel().MapFrom(masterContract);

            await _localizingService.FillModelToEditByAdminAsync(masterContract, model);

            return model;
        }

        public async Task<Result<List<ClientSideMasterContractDetailsViewModel>>> GetMasterContractsAsync()
        {
            var masterContracts = await _masterContractRepository.GetAllAsync(m => m.IsPublished == true);

            if (masterContracts is null || !masterContracts.Any())
            {
                return new List<ClientSideMasterContractDetailsViewModel>();
            }
            var models = masterContracts.Select(s => new ClientSideMasterContractDetailsViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        public async Task<Result<List<ClientSideUploadedMasterContractDetailsViewModel>>> GetUploadedMasterContractsAsync(int masterId)
        {
            var uploadedMasterContracts = await _uploadedMasterContractService.GetUploadMasterContractsAsync(masterId);

            return uploadedMasterContracts;
        }

        #endregion
    }
}
