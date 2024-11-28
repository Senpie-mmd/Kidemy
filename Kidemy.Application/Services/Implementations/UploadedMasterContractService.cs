using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.UploadedMasterContract;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Events.Master;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.Services.Implementations
{
    public class UploadedMasterContractService : IUploadedMasterContractService
    {
        #region Fields

        private readonly IUploadedMasterContractRepository _uploadedMasterContractRepository;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Ctor
        public UploadedMasterContractService(IUploadedMasterContractRepository uploadedMasterContractRepository, IMediatorHandler mediatorHandler, IUserService userService, IMasterService masterService)
        {
            _uploadedMasterContractRepository = uploadedMasterContractRepository;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
            _masterService = masterService;
        }

        #endregion

        #region Methods
        public async Task<Result> CreateAsync(ClientSideCreateUploadedMasterContractViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var isExistMaster = await _masterService.IsExistMasterByIdAsync(model.MasterId);

            if (isExistMaster.Value == false)
            {
                return Result.Failure(ErrorMessages.PleaseFillInformationAboutTheMeacherFirst);
            }

            var uploadedMasterContract = new UploadedMasterContract();
            uploadedMasterContract.MasterId = model.MasterId;
            uploadedMasterContract.MasterContractId = model.MasterContractId;
            uploadedMasterContract.Status = UploadedMasterContractStatus.PendingReview;
            uploadedMasterContract.CreatedDateOnUtc = DateTime.UtcNow;

            if (model.File is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }
            uploadedMasterContract.FileName = await SaveUploadedMasterContractFile(model.File);

            await _uploadedMasterContractRepository.InsertAsync(uploadedMasterContract);
            await _uploadedMasterContractRepository.SaveAsync();

            UploadedMasterContractCreatedEvent uploadedMasterContractCreatedEvent = new(
                        uploadedMasterContract.Id,
                        uploadedMasterContract.MasterId,
                        uploadedMasterContract.MasterContractId,
                        uploadedMasterContract.Status,
                        uploadedMasterContract.FileName

           );

            await _mediatorHandler.PublishEvent(uploadedMasterContractCreatedEvent);
            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> UpdateAsync(ClientSideUpdateUploadedMasterContractViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var uploadedMasterContract = await _uploadedMasterContractRepository.GetByIdAsync(model.Id);

            if (uploadedMasterContract is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.FileName is null && model.File is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            if (model.File is not null)
            {
                uploadedMasterContract.FileName = await SaveUploadedMasterContractFile(model.File!, uploadedMasterContract.FileName);
            }


            var copiedUploadedMasterContract = uploadedMasterContract?.DeepCopy<UploadedMasterContract>();

            uploadedMasterContract.Id = model.Id;
            uploadedMasterContract.MasterId = model.MasterId;
            uploadedMasterContract.MasterContractId = model.MasterContractId;
            uploadedMasterContract.Status = UploadedMasterContractStatus.PendingReview;
            uploadedMasterContract.UpdatedDateOnUtc = DateTime.UtcNow;

            _uploadedMasterContractRepository.Update(uploadedMasterContract);
            await _uploadedMasterContractRepository.SaveAsync();

            UploadedMasterContractUpdatedEvent uploadedMasterContractUpdatedEvent = new(
                        uploadedMasterContract.Id,
                        uploadedMasterContract.MasterId,
                        copiedUploadedMasterContract.MasterId,
                        uploadedMasterContract.MasterContractId,
                        copiedUploadedMasterContract.MasterContractId,
                        uploadedMasterContract.Status,
                        copiedUploadedMasterContract.Status,
                        copiedUploadedMasterContract.FileName,
                        copiedUploadedMasterContract.FileName
           );

            await _mediatorHandler.PublishEvent(uploadedMasterContractUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(List<AdminSideUpdateUploadedMasterContractViewModel> model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var uploadedMasterContracts = await _uploadedMasterContractRepository.GetAllAsync();

            if (uploadedMasterContracts is null) return Result.Failure(ErrorMessages.ProcessFailedError);


            foreach (var item in model)
            {
                var uploadedMasterContract = uploadedMasterContracts.First(b => b.Id == item.UploadedMasterContractId);
                uploadedMasterContract.Status = item.Status;
                uploadedMasterContract.UpdatedDateOnUtc = DateTime.UtcNow;

            }
            // var copiedUploadedMasterContract = uploadedMasterContracts?.DeepCopy<UploadedMasterContract>();

            _uploadedMasterContractRepository.UpdateRange(uploadedMasterContracts);
            await _uploadedMasterContractRepository.SaveAsync();

            // UploadedMasterContractUpdatedEvent uploadedMasterContractUpdatedEvent = new(
            //             uploadedMasterContracts.Id,
            //             uploadedMasterContract.MasterId,
            //             copiedUploadedMasterContract.MasterId,
            //             uploadedMasterContract.MasterContractId,
            //             copiedUploadedMasterContract.MasterContractId,
            //             uploadedMasterContract.Status,
            //             copiedUploadedMasterContract.Status,
            //             copiedUploadedMasterContract.FileName,
            //             copiedUploadedMasterContract.FileName
            //);

            // await _mediatorHandler.PublishEvent(uploadedMasterContractUpdatedEvent);

            return Result.Success();

        }

        private async Task<string> SaveUploadedMasterContractFile(IFormFile uploadedMasterContractFile, string? lastFileName = null)
        {
            string fileName = string.Empty;

            if (uploadedMasterContractFile is not null)
            {
                fileName = Guid.NewGuid() + Path.GetExtension(uploadedMasterContractFile.FileName);
                await uploadedMasterContractFile.AddFilesToServerWithNoFormatChecker(fileName, SiteTools.UploadedMasterContractFilePath);

            }

            return fileName;
        }

        public async Task<Result<List<ClientSideUploadedMasterContractDetailsViewModel>>> GetUploadMasterContractsAsync(int masterId)
        {
            var uploadMasterContracts = await _uploadedMasterContractRepository.GetAllAsync(u => u.MasterId == masterId);

            if (uploadMasterContracts is null || !uploadMasterContracts.Any())
            {
                return new List<ClientSideUploadedMasterContractDetailsViewModel>();
            }
            var models = uploadMasterContracts.Select(s => new ClientSideUploadedMasterContractDetailsViewModel().MapFrom(s)).ToList();

            return models;
        }

        public async Task<Result<bool>> ExistUploadedMasterContractByIdAsync(int masterContractId, int masterId)
        {
            if (masterContractId <= 0) return Result.Failure<bool>(ErrorMessages.ProcessFailedError);

            return await _uploadedMasterContractRepository.AnyAsync(u => u.MasterContractId == masterContractId && u.MasterId == masterId);
        }

        public async Task<Result<int>> GetUploadedMasterContractIdByMasterContractIdAsync(int masterContractId)
        {
            if (masterContractId <= 0) return Result.Failure<int>(ErrorMessages.ProcessFailedError);

            return await _uploadedMasterContractRepository.GetUploadedMasterContractIdByMasterContractId(masterContractId);
        }

        public async Task<Result<FilterMasterInformationForContractsPendingConfirmationViewModel>> FilterAsync(FilterMasterInformationForContractsPendingConfirmationViewModel model)
        {
            if (model is null) return Result.Failure<FilterMasterInformationForContractsPendingConfirmationViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<UploadedMasterContract>();

            await _uploadedMasterContractRepository.FilterMasterContractsByDistinctAsync(model, filterConditions, UploadedMasterContractMapper.MapMasterInformationUploadedMasterContractViewModel);

            if (model.Entities.Any())
            {
                var masterIds = model.Entities.Select(x => x.MasterId).ToList();

                var usersResult = await _userService.GetUserNameAndMobileForMasterByUserId(masterIds);

                if (usersResult.IsFailure)
                {
                    return Result.Failure<FilterMasterInformationForContractsPendingConfirmationViewModel>(usersResult.Message);
                }

                var users = usersResult.Value;

                foreach (var user in users)
                {
                    var master = model.Entities.FirstOrDefault(x => x.MasterId == user.Id);

                    if (master == null) continue;

                    master.MasterFirstName = user.UserFirstName;
                    master.MasterLastName = user.UserLastName;
                    master.MasterMobile = user.UserMobile;
                }
            }

            return model;
        }

        public async Task<Result<List<AdminSideMasterContractsPendingConfirmationDetailViewModel>>> GetMasterContractsPendingConfirmationListByMasterIdAsync(int masterId)
        {
            var uploadedMasterContractList = await _uploadedMasterContractRepository.GetAllAsync(p => p.MasterId == masterId, includeProperties: nameof(UploadedMasterContract.MasterContract));

            if (uploadedMasterContractList is null || !uploadedMasterContractList.Any())
            {
                return new List<AdminSideMasterContractsPendingConfirmationDetailViewModel>();
            }

            var models = uploadedMasterContractList.Select(s => new AdminSideMasterContractsPendingConfirmationDetailViewModel().MapFrom(s)).ToList();

            return models;
        }

        #endregion

    }
}