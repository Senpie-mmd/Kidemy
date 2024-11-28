using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Events.Wallet;
using Kidemy.Domain.Interfaces.Wallet;
using Kidemy.Domain.Models.Wallet;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class WalletService : IWalletService
    {
        #region Fields

        private readonly IWalletTransactionRepository _walletTransactionRepository;
        private readonly ILogger<WalletService> _logger;
        private readonly IUserService _userService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMasterService _masterService;
        #endregion

        #region Constructor

        public WalletService(IWalletTransactionRepository walletTransactionRepository, ILogger<WalletService> logger, IUserService userService, IMediatorHandler mediatorHandler, IMasterService masterService)
        {
            _walletTransactionRepository = walletTransactionRepository;
            _logger = logger;
            _userService = userService;
            _mediatorHandler = mediatorHandler;
            _masterService = masterService;
        }

        #endregion

        #region Methods

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionAsync(ChargeWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);


            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.Amount < 1000)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.AmountIsLessThanMinimumError);
            }

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = false,
                Description = model.Description,
                Balance = model.Balance ?? 0,
                TransactionCase = WalletTransactionCase.ChargeWallet,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionAsync(ClientSideChargeWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.Amount < 1000)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.AmountIsLessThanMinimumError);
            }

            var balanceResult = await GetBalanceAsync(model.UserId.Value);

            var balance = balanceResult.IsSuccess ? balanceResult.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = false,
                Balance = balance,
                TransactionCase = WalletTransactionCase.ChargeWallet,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForBuyVIPPlanAsync(ClientSideChargeWalletForVIPPlanViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.Amount < 1000)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.AmountIsLessThanMinimumError);
            }

            var balanceResult = await GetBalanceAsync(model.UserId.Value);

            var balance = balanceResult.IsSuccess ? balanceResult.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                PlanId = model.PlanId,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = false,
                Balance = balance,
                TransactionCase = WalletTransactionCase.VIPPlan,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForBuyConsultationRequestAsync(ClientSideChargeWalletForConsultationRequestViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.Amount < 1000)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.AmountIsLessThanMinimumError);
            }

            var balanceResult = await GetBalanceAsync(model.UserId.Value);

            var balance = balanceResult.IsSuccess ? balanceResult.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                ConsulationRequestId = model.ConsultationRqeeuestId,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = false,
                Balance = balance,
                TransactionCase = WalletTransactionCase.ConsultationRequest,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }
        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(AdminSideChargeWalletForConsultationRequestViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            if (model.Amount < 1000)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.AmountIsLessThanMinimumError);
            }

            var balanceResult = await GetBalanceAsync(model.UserId.Value);

            var balance = balanceResult.IsSuccess ? balanceResult.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                ConsulationRequestId = model.ConsultationRqeeuestId,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                Balance = balance + model.Amount.Value,
                TransactionCase = WalletTransactionCase.ReturnConsultationRequestMoney,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForPayOrderAsync(ChargeWalletForOrderViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = false,
                OrderId = model.OrderId,
                Balance = balance,
                TransactionCase = WalletTransactionCase.PayOrder,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionForPayOrderCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForCourseCommissionAsync(ChargeWalletForCommissionViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP ?? "-",
                Amount = model.Amount.Value,
                IsSuccess = true,
                CourseId = model.CourseId,
                Balance = balance + model.Amount.Value,
                TransactionCase = WalletTransactionCase.CourseCommission,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ChargeWalletTransactionForPayOrderCreatedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }


        public async Task<Result> ChargeWalletByAdminAsync(ChargeWalletViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.AdminActivity,
                TransactionType = WalletTransactionType.Deposit,
                TransactionWay = WalletTransactionWay.Online,
                Description = model.Description,
                Balance = balance + model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();
            var WalletTransactionEvent =
                new ChargedWalletByAdminEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);
            return Result.Success();
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletByAdminAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            if (balance < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.AdminActivity,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.Online,
                Description = model.Description,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();
            var WalletTransactionEvent =
                new WithdrawedFromWalletByAdminEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);
            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletByUserWithdrawRequestAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            result = await GetBalanceAsync(model.UserId.Value, true);
            var balanceWithAppliedBlockAmount = result.IsSuccess ? result.Value : 0;

            if (balanceWithAppliedBlockAmount < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.WithdrawRequest,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.CardToCard,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new WithdrawFromWalletByUserWithdrawRequestedEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> UndoWalletTransaction(int id)
        {
            if (id <= 0) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var toUndoTransaction = await _walletTransactionRepository.GetByIdAsync(id);

            if (toUndoTransaction is null)
            {
                _logger.LogError("WalletService: UndoWalletTransaction: not found a wallet transaction by id : " + id);
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(toUndoTransaction.UserId);
            var balance = result.IsSuccess ? result.Value : 0;

            var expectedTransactionType = toUndoTransaction.TransactionType is WalletTransactionType.Deposit
                    ? WalletTransactionType.Withdraw
                    : WalletTransactionType.Deposit;

            var expectedBalance = expectedTransactionType is WalletTransactionType.Deposit
                ? balance + toUndoTransaction.Amount
                : balance - toUndoTransaction.Amount;

            var walletTransaction = new WalletTransaction
            {
                UserId = toUndoTransaction.UserId,
                IP = toUndoTransaction.IP,
                Amount = toUndoTransaction.Amount,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.UndoTransaction,
                TransactionType = expectedTransactionType,
                TransactionWay = WalletTransactionWay.Online,
                Balance = expectedBalance
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new UndoedWalletTransactionEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForOrderPaymentAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            result = await GetBalanceAsync(model.UserId.Value, true);
            var balanceWithAppliedBlockAmount = result.IsSuccess ? result.Value : 0;

            if (balanceWithAppliedBlockAmount < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }

            if (balance < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }

            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.PayOrder,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.Online,
                OrderId = model.OrderId,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new WithdrawedFromWalletForOrderPaymentEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForBuyVIPPlanAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            result = await GetBalanceAsync(model.UserId.Value, true);
            var balanceWithAppliedBlockAmount = result.IsSuccess ? result.Value : 0;

            if (balanceWithAppliedBlockAmount < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }


            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.VIPPlan,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.Online,
                OrderId = model.OrderId,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new WithdrawedFromWalletForOrderPaymentEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForBuyConsultationRequestAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            result = await GetBalanceAsync(model.UserId.Value, true);
            var balanceWithAppliedBlockAmount = result.IsSuccess ? result.Value : 0;

            if (balanceWithAppliedBlockAmount < model.Amount)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }


            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                ConsulationRequestId = model.ConsultationRequestId,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.ConsultationRequest,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.Online,
                OrderId = model.OrderId,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new WithdrawedFromWalletForOrderPaymentEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForReturnConsultationRequestMoneyAsync(WithdrawFromWalletViewModel model)
        {
            if (model is null) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            if (model.UserId <= 0 || model.Amount <= 0)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            var result = await GetBalanceAsync(model.UserId.Value);
            var balance = result.IsSuccess ? result.Value : 0;

            if (balance < model.Amount.Value)
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
            }


            var walletTransaction = new WalletTransaction
            {
                UserId = model.UserId.Value,
                IP = model.UserIP,
                Amount = model.Amount.Value,
                ConsulationRequestId = model.ConsultationRequestId,
                IsSuccess = true,
                TransactionCase = WalletTransactionCase.ReturnConsultationRequestMoney,
                TransactionType = WalletTransactionType.Withdraw,
                TransactionWay = WalletTransactionWay.Online,
                OrderId = model.OrderId,
                Balance = balance - model.Amount.Value
            };

            await _walletTransactionRepository.InsertAsync(walletTransaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new WithdrawedFromWalletForOrderPaymentEvent
                (
                    walletTransaction.Id,
                    walletTransaction.UserId,
                    walletTransaction.OrderId,
                    walletTransaction.IP,
                    walletTransaction.Amount,
                    walletTransaction.Balance,
                    walletTransaction.IsSuccess,
                    walletTransaction.RefId,
                    walletTransaction.Description,
                    walletTransaction.TransactionType,
                    walletTransaction.TransactionCase,
                    walletTransaction.TransactionWay
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        
        public async Task<Result> ConfirmPaidTransactionAsync(int id, string refId)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            var transaction = await _walletTransactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                _logger.LogError($"Not found wallet transaction to confirm paid, expected id : {id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var lastBalanceResult = await GetBalanceAsync(transaction.UserId);

            if (lastBalanceResult.IsFailure) return lastBalanceResult;

            if (transaction.TransactionType is WalletTransactionType.Deposit)
            {
                transaction.Balance = lastBalanceResult.Value + transaction.Amount;
            }
            else if (transaction.TransactionType is WalletTransactionType.Withdraw)
            {
                var result = await GetBalanceAsync(transaction.UserId, true);
                var balanceWithAppliedBlockAmount = result.IsSuccess ? result.Value : 0;

                if (balanceWithAppliedBlockAmount < transaction.Amount)
                {
                    return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.NotEnoughBalanceError);
                }

                transaction.Balance = lastBalanceResult.Value - transaction.Amount;
            }

            transaction.IsSuccess = true;
            transaction.RefId = refId;
            transaction.UpdatedDateOnUtc = DateTime.UtcNow;

            _walletTransactionRepository.Update(transaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new ConfirmedPaidTransactionEvent
                (
                    transaction.Id,
                    transaction.RefId,
                    transaction.IsSuccess,
                    transaction.Balance

                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return Result.Success();
        }

        public async Task<Result> SetReferenceIdForTransaction(int id, string refId)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(refId)) return Result.Failure(ErrorMessages.ProcessFailedError);

            var transaction = await _walletTransactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                _logger.LogError($"Not found wallet transaction to confirm paid, expected id : {id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            if (transaction.RefId == refId)
            {
                return Result.Success();
            }
            else if (!string.IsNullOrWhiteSpace(transaction.RefId))
            {
                return Result.Failure(ErrorMessages.TransactionAlreadyHasRefIdError);
            }

            transaction.RefId = refId;
            transaction.UpdatedDateOnUtc = DateTime.UtcNow;

            _walletTransactionRepository.Update(transaction);
            await _walletTransactionRepository.SaveAsync();

            var WalletTransactionEvent =
                new SetedReferenceIdForTransactionEvent
                (
                    transaction.Id,
                    transaction.RefId
                );

            await _mediatorHandler.PublishEvent(WalletTransactionEvent);

            return Result.Success();
        }

        public async Task<Result<decimal>> GetBalanceAsync(int userId, bool applyBlockedAmount = false)
        {
            if (userId <= 0) return Result.Failure<decimal>(ErrorMessages.ProcessFailedError);

            var lastTransaction = await _walletTransactionRepository
                        .LastOrDefaultAsync(t => t.UserId == userId && t.IsSuccess,
                                            orderBy: t => t.UpdatedDateOnUtc);

            var balance = lastTransaction?.Balance ?? 0;

            if (applyBlockedAmount && balance > 0)
            {
                var blockedAmountResult = await _masterService.GetBlockedBalanceAsync(userId);

                if (blockedAmountResult.IsSuccess)
                {
                    balance -= blockedAmountResult.Value.BlockedAmount ?? 0;

                    balance = balance < 0 ? 0 : balance;
                }
            }

            return balance;
        }

        public async Task<Result<FilterWalletTransactionViewModel>> FilterAsync(FilterWalletTransactionViewModel model)
        {
            if (model is null) return Result.Failure<FilterWalletTransactionViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<WalletTransaction>();

            if (model.UserId is not null)
            {
                filterConditions.Add(t => t.UserId == model.UserId);
            }

            await _walletTransactionRepository.FilterAsync(model, filterConditions, WalletMapper.MapWalletTransactionViewModel);

            //ToDO: Get UserFullName from user service

            var userIds = new List<int>();
            model.Entities.ForEach(e => userIds.Add(e.UserId));
            List<UserFullNameModel> list = await _userService.GetUsersFullNames(userIds);


            foreach (var transactionViewModel in model.Entities)
            {
                transactionViewModel.UserFullName =
                    list.FirstOrDefault(u => u.UserId == transactionViewModel.UserId)?.UserFullName ?? "-";
            }

            return model;
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var walletTransaction = await _walletTransactionRepository.GetByIdAsync(id);

            if (walletTransaction is null)
            {
                _logger.LogError($"Not found wallet transaction by id : {id}");
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByOrderIdAsync(int orderId)
        {
            if (orderId <= 0) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var walletTransaction = await _walletTransactionRepository.FirstOrDefaultAsync(t => t.OrderId == orderId);

            if (walletTransaction is null)
            {
                _logger.LogError($"Not found wallet transaction by orderId : {orderId}");
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);

        }
        public async Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByuserIdAsync(WalletTransactionDetailsViewModel model)
        {
            if (model.UserId <= 0 || model.ConsulationRequestId <= 0  ) return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var walletTransaction = await _walletTransactionRepository.FirstOrDefaultAsync(t => t.ConsulationRequestId == model.ConsulationRequestId && t.UserId == model.UserId && t.TransactionType == model.TransactionType);

            if (walletTransaction is null)
            {
                _logger.LogError($"Not found wallet transaction by consulationRequestId : {model.ConsulationRequestId} And {model.UserId} And {model.TransactionType}");
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.ProcessFailedError);
            }

            return new WalletTransactionDetailsViewModel().MapFrom(walletTransaction);

        }

        public async Task<Result<decimal>> GetWalletTransactionAmountAsync(int id)
        {
            if (id <= 0) return Result.Failure<decimal>(ErrorMessages.ProcessFailedError);

            var transaction = await _walletTransactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                _logger.LogError($"Not found transaction by id: {id}");
                return Result.Failure<decimal>(ErrorMessages.ProcessFailedError);
            }

            return transaction.Amount;
        }

        #endregion
    }
}
