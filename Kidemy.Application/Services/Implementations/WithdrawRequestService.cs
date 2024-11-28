using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Events.WithrawRequest;
using Kidemy.Domain.Interfaces.User;
using Kidemy.Domain.Interfaces.WithdrawRequest;
using Kidemy.Domain.Models.WithdrawRequest;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GoftemanMelal.Application.Services.Implementations
{
    public class WithdrawRequestService : IWithdrawRequestService
    {
        #region Fields

        private readonly IWithdrawRequestRepository _withdrawRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<WithdrawRequestService> _logger;
        private readonly ISiteSettingService _siteSettingsService;
        private readonly IWalletService _walletService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;

        #endregion

        #region Constructor
        public WithdrawRequestService(IWithdrawRequestRepository withdrawRequestRepository, ILogger<WithdrawRequestService> logger, IUserRepository userRepository, ISiteSettingService siteSettingsService, IWalletService walletService, IHttpContextAccessor httpContextAccessor, IMediatorHandler mediatorHandler, IUserService userService, IMasterService masterService)
        {
            _withdrawRequestRepository = withdrawRequestRepository;
            _logger = logger;
            _userRepository = userRepository;
            _siteSettingsService = siteSettingsService;
            _walletService = walletService;
            _httpContextAccessor = httpContextAccessor;
            _mediatorHandler = mediatorHandler;
            _userService = userService;
            _masterService = masterService;
        }

        #endregion

        #region Methods

        public async Task<Result> CreateAsync(UpsertWithdrawRequestViewModel model)
        {
            if (model == null)
            {
                _logger.LogError("WithdrawRequestService: Create: model is null");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var masterIsConfirmed = await _masterService.MasterIsConfirmed(model.UserId);

            if (masterIsConfirmed.IsFailure)
            {
                return Result.Failure(ErrorMessages.ThePossibilityOfRequestingASettlementError);
            }

            var siteSettingResult = await _siteSettingsService.GetSiteSettingAsync();

            if (siteSettingResult.IsFailure) return siteSettingResult;

            var siteSetting = siteSettingResult.Value;

            if (model.Amount < siteSetting.WithdrawRequestMinimumAmount)
            {
                return Result.Failure(ErrorMessages.MinimumWithdrawRequestAmountError);
            }

            var withdrawRequestResult = await _walletService.WithdrawFromWalletByUserWithdrawRequestAsync(new WithdrawFromWalletViewModel
            {
                Amount = model.Amount,
                UserId = _httpContextAccessor.HttpContext.User.GetUserId(),
                UserIP = _httpContextAccessor.HttpContext.GetUserIP(),
            });

            if (withdrawRequestResult.IsFailure)
            {
                return Result.Failure(withdrawRequestResult.Message!);
            }

            var withdrawRequestTransaction = withdrawRequestResult.Value;

            var withdrawRequest = new WithdrawRequest
            {
                UserId = _httpContextAccessor.HttpContext.User.GetUserId(),
                Status = WithdrawRequestStatus.Pending,
                Amount = model.Amount.GetValueOrDefault(),
                DestinationBankAccountCardId = model.DestinationBankAccountCardId.GetValueOrDefault(),
                WalletTransactionId = withdrawRequestTransaction.Id
            };


            await _withdrawRequestRepository.InsertAsync(withdrawRequest);
            await _withdrawRequestRepository.SaveAsync();


            WithdrawRequestCreatedEvent withdrawRequestCreatedEvent = new(
                                                                          withdrawRequest.UserId,
                                                                          withdrawRequest.Status,
                                                                          withdrawRequest.Amount,
                                                                          withdrawRequest.DestinationBankAccountCardId,
                                                                          withdrawRequest.WalletTransactionId);

            await _mediatorHandler.PublishEvent(withdrawRequestCreatedEvent);


            return Result.Success();
        }

        public async Task<Result<FilterWithdrawRequestViewModel>> FilterWithdrawRequestAsync(FilterWithdrawRequestViewModel model)
        {
            if (model is null)
            {
                return Result.Failure<FilterWithdrawRequestViewModel>(ErrorMessages.ProcessFailedError);
            }

            var conditions = Filter.GenerateConditions<WithdrawRequest>();

            if (model.UserName != null)
            {
                var users = await _userService.GetUserIdByuserName(model.UserName);
                if (users.IsSuccess)
                    conditions.Add(x => users.Value.Any(user => user.Id == x.Id));
            }


            if (model.Status != null)
            {
                conditions.Add(x => x.Status == model.Status);
            }

            if (model.UserId is not null)
            {
                conditions.Add(x => x.UserId == model.UserId);
            }
            await _withdrawRequestRepository.FilterAsync(model, conditions, WithdrawRequestMapper.MapAdminSideWithdrawRequestViewModel, orderByDesc: ac => ac.UpdatedDateOnUtc);



            var userIds = model.Entities.Select(n => n.UserId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(withdraw => withdraw
                .MapFrom(usersInfo.Value.First(n => n.Id == withdraw.UserId)))
                .ToList();

            return model;
        }

        public async Task<Result> RejectAsync(RejectWithdrawRequestViewModel model)
        {
            if (model is null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var withdrawRequest = await _withdrawRequestRepository.GetByIdAsync(model.Id);

            if (withdrawRequest == null || withdrawRequest.Status != WithdrawRequestStatus.Pending)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var undoResult = await _walletService.UndoWalletTransaction(withdrawRequest.WalletTransactionId);

            if (undoResult.IsFailure)
            {
                return undoResult;
            }

            withdrawRequest.Status = WithdrawRequestStatus.Rejected;
            withdrawRequest.Description = model.Description;
            withdrawRequest.UpdatedDateOnUtc = DateTime.UtcNow;

            _withdrawRequestRepository.Update(withdrawRequest);
            await _withdrawRequestRepository.SaveAsync();

            WithrawRequestAcceptedEvent withrawRequestAcceptedEvent = new(
             withdrawRequest.Status,
             withdrawRequest.Description,
             withdrawRequest.RefId);

            await _mediatorHandler.PublishEvent(withrawRequestAcceptedEvent);

            return Result.Success();
        }

        public async Task<Result> AcceptAsync(AcceptWithdrawRequestViewModel model)
        {
            if (model is null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var withdrawRequest = await _withdrawRequestRepository.GetByIdAsync(model.Id);

            if (withdrawRequest == null || withdrawRequest.Status != WithdrawRequestStatus.Pending)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            withdrawRequest.Status = WithdrawRequestStatus.Accepeted;
            withdrawRequest.Description = model.Description;
            withdrawRequest.UpdatedDateOnUtc = DateTime.UtcNow;
            withdrawRequest.RefId = model.RefId;


            _withdrawRequestRepository.Update(withdrawRequest);
            await _withdrawRequestRepository.SaveAsync();


            WithrawRequestAcceptedEvent withrawRequestAcceptedEvent = new(
               withdrawRequest.Status,
               withdrawRequest.Description,
               withdrawRequest.RefId);

            await _mediatorHandler.PublishEvent(withrawRequestAcceptedEvent);

            return Result.Success();
        }

        public async Task<Result<int>> GetWithdrawRequestCountAsync(WithdrawRequestStatus status)
            => await _withdrawRequestRepository.GetCountAsync(request => request.Status == status);

        #endregion
    }
}
