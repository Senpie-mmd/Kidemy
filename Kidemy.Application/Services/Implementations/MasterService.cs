using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.DTOs.Master;
using Kidemy.Domain.Enums.Master;
using Kidemy.Domain.Events.Master;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Models.Master;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Kidemy.Application.Services.Implementations
{
    public class MasterService : IMasterService
    {
        #region Fields

        private readonly IMasterRepository _masterRepository;
        private readonly IUserService _userService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Ctor
        public MasterService(IMasterRepository masterRepository, IMediatorHandler mediatorHandler, IUserService userService)
        {
            _masterRepository = masterRepository;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
        }

        #endregion

        #region Methods
        public async Task<Result> CreateAsync(ClientSideRegisterMasterViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var master = new Master();
            master.Id = model.Id;
            master.Bio = model.Bio;
            master.FatherName = model.FatherName;
            master.NationalIDNumber = model.NationalIDNumber;
            master.IdentificationNumber = model.IdentificationNumber;
            master.PlaceOfIssuance = model.PlaceOfIssuance;
            master.PostalCode = model.PostalCode;
            master.Address = model.Address;

            if (model.IdentificationFile is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }
            master.IdentificationFileName = SaveMasterIdentificationImage(model.IdentificationFile);

            await _masterRepository.InsertAsync(master);
            await _masterRepository.SaveAsync();

            MasterCreatedEvent masterCreatedEvent = new(
                        master.Id,
                        master.Bio,
                        master.FatherName,
                        master.NationalIDNumber,
                        master.IdentificationNumber,
                        master.PlaceOfIssuance,
                        master.PostalCode,
                        master.Address,
                        master.IdentificationFileName
           );

            await _mediatorHandler.PublishEvent(masterCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> CreateAsync(int id)
        {
            var master = new Master();
            master.Id = id;

            await _masterRepository.InsertAsync(master);
            await _masterRepository.SaveAsync();

            MasterCreatedEvent masterCreatedEvent = new(
                        master.Id,
                        master.Bio,
                        master.FatherName,
                        master.NationalIDNumber,
                        master.IdentificationNumber,
                        master.PlaceOfIssuance,
                        master.PostalCode,
                        master.Address,
                        master.IdentificationFileName
           );

            await _mediatorHandler.PublishEvent(masterCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(ClientSideUpdateMasterViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(model.Id);

            if (master is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.IdentificationFileName is null && model.IdentificationFile is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            var copiedMaster = master?.DeepCopy<Master>();

            if (model.IdentificationFile is not null)
            {
                model.IdentificationFileName = SaveMasterIdentificationImage(model.IdentificationFile!, master.IdentificationFileName);

                if (string.IsNullOrEmpty(model.IdentificationFileName))
                {
                    return Result.Failure(ErrorMessages.ImageIsNotValidError);
                }
            }

            master.Id = model.Id;
            master.Bio = model.Bio;
            master.FatherName = model.FatherName;
            master.NationalIDNumber = model.NationalIDNumber;
            master.IdentificationNumber = model.IdentificationNumber;
            master.PlaceOfIssuance = model.PlaceOfIssuance;
            master.PostalCode = model.PostalCode;
            master.Address = model.Address;
            master.IdentificationFileName = model.IdentificationFileName;
            master.Status = MasterStatus.Pending;


            _masterRepository.Update(master);
            await _masterRepository.SaveAsync();

            MasterUpdatedEvent masterUpdatedEvent = new(
                        master.Id,
                        master.Bio,
                        copiedMaster.Bio,
                        master.FatherName,
                        copiedMaster.FatherName,
                        master.NationalIDNumber,
                        copiedMaster.NationalIDNumber,
                        master.IdentificationNumber,
                        copiedMaster.IdentificationNumber,
                        master.PlaceOfIssuance,
                        copiedMaster.PlaceOfIssuance,
                        master.PostalCode,
                        copiedMaster.PostalCode,
                        master.Address,
                        copiedMaster.Address,
                        master.IdentificationFileName,
                        copiedMaster.IdentificationFileName
           );

            await _mediatorHandler.PublishEvent(masterUpdatedEvent);

            return Result.Success();

        }

        public async Task<Result<ClientSideUpdateMasterViewModel>> GetMasterForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<ClientSideUpdateMasterViewModel>(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(id);

            if (master is null) return Result.Failure<ClientSideUpdateMasterViewModel>(ErrorMessages.ProcessFailedError);

            var model = new ClientSideUpdateMasterViewModel().MapFrom(master);

            return model;

        }

        public async Task<Result<bool>> IsExistMasterByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<bool>(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(id);

            if (master is null) return false;

            return true;

        }

        private static string SaveMasterIdentificationImage(IFormFile masterImage, string? lastImageName = null)
        {
            string imageName = string.Empty;

            if (masterImage.IsImage())
            {
                imageName = Guid.NewGuid() + Path.GetExtension(masterImage.FileName);

                if (lastImageName is not null)
                {
                    if (masterImage.IsSVG())
                    {
                        masterImage.AddImageToServerWithOutThumb(imageName, SiteTools.MasterImagePath, 400, 300, lastImageName);
                    }
                    else
                    {
                        masterImage.AddImageToServer(imageName, SiteTools.MasterImagePath, 400, 300,
                            SiteTools.MasterImageThumbPath, lastImageName);
                    }
                }
                else
                {
                    if (masterImage.IsSVG())
                    {
                        masterImage.AddImageToServerWithOutThumb(imageName, SiteTools.MasterImagePath, 400, 300);
                    }
                    else
                    {
                        masterImage.AddImageToServer(imageName, SiteTools.MasterImagePath, 400, 300,
                            SiteTools.MasterImageThumbPath);
                    }
                }
            }

            return imageName;
        }

        public async Task<Result<FilterForAdminSideMasterViewModel>> FilterForAdminSideAsync(FilterForAdminSideMasterViewModel model)
        {
            if (model == null) return Result.Failure<FilterForAdminSideMasterViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Master>();

            filterConditions.Add(m => m.User.Roles.Any(r => r.RoleId == Roles.Master));

            if (model.UserId is not null)
            {
                filterConditions.Add(m => m.Id == model.UserId);
            }

            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                filterConditions.Add(m => m.User.FirstName.Contains(model.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                filterConditions.Add(m => m.User.LastName.Contains(model.LastName));
            }
            if (!string.IsNullOrWhiteSpace(model.Mobile))
            {
                filterConditions.Add(m => m.User.Mobile.Contains(model.Mobile));
            }

            if (!string.IsNullOrWhiteSpace(model.NationalIDNumber))
            {
                filterConditions.Add(m => m.NationalIDNumber.Contains(model.NationalIDNumber));
            }

            await _masterRepository.FilterAsync(model, filterConditions, MasterMapper.AdminMapMasterViewModel);

            return model;
        }

        public async Task<Result<AdminSideMasterDetailsViewModel>> GetMasterInfoAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideMasterDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(id);

            if (master is null) return Result.Failure<AdminSideMasterDetailsViewModel>(ErrorMessages.ProcessFailedError);


            return new AdminSideMasterDetailsViewModel().MapFrom(master);
        }

        public async Task<Result> ChangeStatusAsync(int id, MasterStatus masterStatus)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(id);

            if (master is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            master.Status = masterStatus;

            _masterRepository.Update(master);
            await _masterRepository.SaveAsync();

            return Result.Success();


        }

        public async Task<Result<List<ClientSideBioForMasterModel>>> GetMasterBioByUserId(List<int> ids)
        {
            if (ids.Count() < 1) return Result.Failure<List<ClientSideBioForMasterModel>>(ErrorMessages.FailedOperationError);

            var masterList = new List<ClientSideBioForMasterModel>();
            foreach (var id in ids)
            {
                var master = await _masterRepository.FirstOrDefaultAsync(x => x.User.Id == id, includeProperties: nameof(Master.User));
                if (master != null)
                {
                    var model = new ClientSideBioForMasterModel()
                    {
                        Bio = master.Bio ?? " ",
                        UserId = id,
                    };
                    masterList.Add(model);
                }
            }

            return masterList;
        }
        public async Task<Result<FilterForClientSideMasterViewModel>> FilterForClientSideAsync(FilterForClientSideMasterViewModel model)
        {
            if (model == null) return Result.Failure<FilterForClientSideMasterViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Master>();

            filterConditions.Add(m => m.User.Roles.Any(r => r.RoleId == Roles.Master));
            filterConditions.Add(m => m.Status == MasterStatus.Confirmed);

            if (!string.IsNullOrWhiteSpace(model.FullName))
            {
                filterConditions.Add(m => (m.User.FirstName + " " + m.User.LastName).Contains(model.FullName));
            }

            await _masterRepository.FilterAsync(model, filterConditions, MasterMapper.ClientMapMasterViewModel);

            return model;
        }

        public async Task<Result<ClientSideMasterDetailsViewModel>> GetMasterDetailsByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<ClientSideMasterDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var master = await _masterRepository.GetByIdAsync(id, includeProperties: nameof(Master.User));

            if (master is null) return Result.Failure<ClientSideMasterDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var model = new ClientSideMasterDetailsViewModel().MapFrom(master);

            return model;
        }


        public async Task<Result> SetSetBlockedAmount(BlockedAmountViewModel model)
        {
            if (model == null) return Result.Failure(ErrorMessages.NullValue);

            var master = await _masterRepository.FirstOrDefaultAsync(x => x.Id == model.MasterId);

            master.BlockedAmount = model.BlockedAmount;

            _masterRepository.Update(master);

            await _masterRepository.SaveAsync();
            return Result.Success();
        }

        public async Task<Result<BlockedAmountViewModel>> GetBlockedBalanceAsync(int userId)
        {
            var master = await _masterRepository.FirstOrDefaultAsync(x => x.User.Id == userId, includeProperties: nameof(Master.User));

            if (master is null) return Result.Failure<BlockedAmountViewModel>(ErrorMessages.NullValue);

            var blockedAmount = new BlockedAmountViewModel()
            {
                MasterId = master.Id,
                UserId = userId,
                BlockedAmount = master.BlockedAmount,
            };

            return blockedAmount;
        }

        public async Task<Result<List<int>>> GetMastersIds()
        {
            return await _masterRepository.GetMastersIds();
        }
        public async Task<Result<int>> GetCountAsync(MasterStatus status)
            => await _masterRepository.GetCountAsync(request => request.Status == status);


        public async Task<Result<bool>> MasterIsConfirmed(int id)
        {
            var result = await _masterRepository.MasterIsConfirmed(id);

            if (!result)
            {
                return Result.Failure<bool>(ErrorMessages.MasterIsNotConfirmedError);
            }

            return result;
        }

        #endregion
    }
}