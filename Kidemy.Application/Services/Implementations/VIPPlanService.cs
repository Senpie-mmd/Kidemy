using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Events.VIPPlan;
using Kidemy.Domain.Interfaces.VIPMembers;
using Kidemy.Domain.Models.VIPMembers;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class VIPPlanService : IVIPPlanService
    {
        #region Fields

        private readonly IVIPPlanRepository _vIPPlanRepository;
        private readonly IWalletService _walletService;
        private readonly IVIPMembersService _vIPMembersService;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public VIPPlanService(
            IVIPPlanRepository vIPPlanRepository,
            ILocalizingService localizingService,
            IVIPMembersService vIPMembersService,
            IMediatorHandler mediatorHandler,
            IWalletService walletService)
        {
            _vIPPlanRepository = vIPPlanRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
            _walletService = walletService;
            _vIPMembersService = vIPMembersService;
        }

        #endregion

        #region Methods
        public async Task<Result<AdminSideFilterVIPPlanViewModel>> FilterAsync(AdminSideFilterVIPPlanViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterVIPPlanViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<VIPPlan>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _vIPPlanRepository.FilterAsync(model, filterConditions, VIPPlanMapper.MapVIPPlanViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;

        }

        public async Task<Result> CreateAsync(AdminSideUpsertVIPPlanViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            VIPPlan vIPPlan = new()
            {
                Title = model.Title,
                Price = model.Price,
                DurationDay = model.DurationDay,
                IsPublished = model.IsPublished,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            await _vIPPlanRepository.InsertAsync(vIPPlan);
            await _vIPPlanRepository.SaveAsync();

            await _localizingService.SaveLocalizations(vIPPlan, model);

            VIPPlanCreatedEvent vIPPlanCreatedEvent = new(
                vIPPlan.Title,
                vIPPlan.DurationDay,
                vIPPlan.Price,
                vIPPlan.IsPublished);

            await _mediatorHandler.PublishEvent(vIPPlanCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertVIPPlanViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var vIPPlan = await _vIPPlanRepository.GetByIdAsync(model.Id);

            if (vIPPlan is null) return Result.Failure(ErrorMessages.NullValue);

            var copiedVIPPlan = vIPPlan.DeepCopy<VIPPlan>();

            vIPPlan.Title = model.Title;
            vIPPlan.Price = model.Price;
            vIPPlan.DurationDay = model.DurationDay;
            vIPPlan.IsPublished = model.IsPublished;

            _vIPPlanRepository.Update(vIPPlan);
            await _vIPPlanRepository.SaveAsync();

            await _localizingService.SaveLocalizations(vIPPlan, model);

            VIPPlanUpdatedEvent vIPPlanUpdatedEvent = new(
                vIPPlan.Id,
                vIPPlan.Title,
                copiedVIPPlan.Title,
                vIPPlan.DurationDay,
                copiedVIPPlan.DurationDay,
                vIPPlan.Price,
                copiedVIPPlan.Price,
                vIPPlan.IsPublished,
                copiedVIPPlan.IsPublished);


            await _mediatorHandler.PublishEvent(vIPPlanUpdatedEvent);

            return Result.Success();

        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.FailedOperationError);

            await _vIPPlanRepository.SoftDelete(id);
            await _vIPPlanRepository.SaveAsync();


            VIPPlanDeletedEvent vIPPlanDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(vIPPlanDeletedEvent);

            return Result.Success();

        }

        public async Task<Result<AdminSideUpsertVIPPlanViewModel>> GetVIPPlanForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertVIPPlanViewModel>(ErrorMessages.FailedOperationError);

            var vIPPlan = await _vIPPlanRepository.GetByIdAsync(id);

            if (vIPPlan is null) return Result.Failure<AdminSideUpsertVIPPlanViewModel>(ErrorMessages.FailedOperationError);

            var model = new AdminSideUpsertVIPPlanViewModel().MapFrom(vIPPlan);

            await _localizingService.FillModelToEditByAdminAsync(vIPPlan, model);

            return model;

        }

        public async Task<Result<List<ClientSideVIPPlanViewModel>>> GetAllVIPPlans()
        {
            var vIPPlans = await _vIPPlanRepository.GetAllAsync(v => v.IsPublished == true);

            if (vIPPlans is null || !vIPPlans.Any())
            {
                return new List<ClientSideVIPPlanViewModel>();
            }

            var models = vIPPlans.Select(s => new ClientSideVIPPlanViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        public async Task<Result<decimal>> GetAmountById(int id)
        {
            if (id <= 0) return Result.Failure<decimal>(ErrorMessages.ProcessFailedError);

            var amount = await _vIPPlanRepository.GetAmountById(id);
            return amount;


        }

        public async Task<Result<WalletTransactionDetailsViewModel>> BuyVIPPlanAsync(ClientSideBuyVIPPlanViewModel model)
        {
            var vIPUserInformation = await _vIPMembersService.GetVIPUserInformation(model.UserId.Value);


            var amount = await GetAmountById(model.PlanId);
            var balance = await _walletService.GetBalanceAsync(model.UserId.Value, true);

            if (!model.UseFromWalletBalnace || balance.Value < amount.Value)
            {
                if (model.UseFromWalletBalnace)
                {
                    amount = amount.Value - balance.Value;
                }

                ClientSideChargeWalletForVIPPlanViewModel chargeWalletForVIPPlanViewModel = new()
                {
                    Amount = amount.Value,
                    UserId = model.UserId.Value,
                    UserIP = model.UserIP,
                    PlanId = model.PlanId
                };

                return await _walletService.CreateChargeWalletTransactionForBuyVIPPlanAsync(chargeWalletForVIPPlanViewModel);
            }
            else
            {
                ClientSideConfirmPlanViewModel confirmPlanViewModel = new()
                {
                    UserId = model.UserId.Value,
                    PlanId = model.PlanId,
                    UserIp = model.UserIP

                };

                return await ConfirmPlanForUser(confirmPlanViewModel);
            }
        }

        public async Task<Result<WalletTransactionDetailsViewModel>> ConfirmPlanForUser(ClientSideConfirmPlanViewModel model)
        {
            var amount = await GetAmountById(model.PlanId);
            var balance = await _walletService.GetBalanceAsync(model.UserId, true);

            if (amount.Value <= balance.Value)
            {
                WithdrawFromWalletViewModel withdrawFromWalletViewModel = new()
                {
                    Amount = amount.Value,
                    UserId = model.UserId,
                    PlanId = model.PlanId,
                    UserIP = model.UserIp

                };

                var withdrawFromWallet = await _walletService.WithdrawFromWalletForBuyVIPPlanAsync(withdrawFromWalletViewModel);

                int durationDay = await _vIPPlanRepository.GetDurationDayById(model.PlanId);
                DateTime nowDateTime = DateTime.UtcNow;
                DateTime membershipEndDate = nowDateTime.AddDays(durationDay);

                ClientSideJoinVIPMembersViewModel joinVIPMembersViewModel = new()
                {
                    UserId = model.UserId,
                    VIPPlanId = model.PlanId,
                    MembershipStartDate = DateTime.UtcNow,
                    MembershipEndDate = membershipEndDate,

                };

                await _vIPMembersService.JoinVIPMembersAsync(joinVIPMembersViewModel);

                return withdrawFromWallet;


            }
            else
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);
            }

        }

        public async Task<Result<List<AdminSideVIPPlanViewModel>>> GetAllVIPPlansForAdmin()
        {
            var vIPPlans = await _vIPPlanRepository.GetAllAsync(v => v.IsPublished == true);

            if (vIPPlans is null || !vIPPlans.Any())
            {
                return new List<AdminSideVIPPlanViewModel>();
            }

            var models = vIPPlans.Select(s => new AdminSideVIPPlanViewModel().MapFrom(s)).ToList();

            await _localizingService.TranslateModelAsync(models);

            return models;
        }

        public async Task<Result> AssignPlanForUserByAdminAsync(int PlanId, int UserId)
        {
            // if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            int durationDay = await _vIPPlanRepository.GetDurationDayById(PlanId);
            DateTime nowDateTime = DateTime.UtcNow;
            DateTime membershipEndDate = nowDateTime.AddDays(durationDay);

            ClientSideJoinVIPMembersViewModel joinVIPMembersViewModel = new()
            {
                UserId = UserId,
                VIPPlanId = PlanId,
                MembershipStartDate = DateTime.UtcNow,
                MembershipEndDate = membershipEndDate,

            };

            await _vIPMembersService.JoinVIPMembersAsync(joinVIPMembersViewModel);
            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion
    }
}
