using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Enums.Consultation;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Events.Consultation.ConsultationRequest;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Kidemy.Domain.Models.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class ConsultationRequestService : IConsultationRequestService
    {

        #region Fields
        private readonly IConsultationRequestRepository _consultationRequestRepository;
        private readonly IWalletService _walletService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAdviserAvailableDateRepository _adviserAvailableDateRepository;
        private readonly IAdviserConsultationTypeRespositry _adviserConsultationTypeRespositry;
        private readonly IUserService _userService;
        private readonly IAdviserSerivce _adviserSerivce;

        #endregion

        #region Constructor

        public ConsultationRequestService(IConsultationRequestRepository consultationRequestRepository, IWalletService walletService, IMediatorHandler mediatorHandler, IAdviserConsultationTypeRespositry adviserConsultationTypeRespositry, IAdviserAvailableDateRepository adviserAvailableDateRepository, IUserService userService, IAdviserSerivce adviserSerivce)
        {
            _consultationRequestRepository = consultationRequestRepository;
            _walletService = walletService;
            _mediatorHandler = mediatorHandler;
            _adviserConsultationTypeRespositry = adviserConsultationTypeRespositry;
            _adviserAvailableDateRepository = adviserAvailableDateRepository;
            _userService = userService;
            _adviserSerivce = adviserSerivce;
        }
        #endregion

        #region Methods

        #region Filter
        public async Task<Result<AdminSideFilterConsultationRequestViewModel>> AdminSideFilterAsync(AdminSideFilterConsultationRequestViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterConsultationRequestViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<ConsultationRequest>();

            switch (model.State)
            {
                case FilterConsultationRequestState.All:
                    break;
                case FilterConsultationRequestState.WaitingForSetTime:
                    filterConditions.Add(x => x.State == ConsultationRequestState.WaitingForSetTime);
                    break;
                case FilterConsultationRequestState.Canceled:
                    filterConditions.Add(x => x.State == ConsultationRequestState.Canceled);
                    break;
                case FilterConsultationRequestState.WaitingForEvent:
                    filterConditions.Add(x => x.State == ConsultationRequestState.WaitingForEvent);
                    break;
                case FilterConsultationRequestState.Finished:
                    filterConditions.Add(x => x.State == ConsultationRequestState.Finished);
                    break;
                case FilterConsultationRequestState.WaitingForPayment:
                    filterConditions.Add(x => x.State == ConsultationRequestState.WaitingForPayment);
                    break;
            }


            await _consultationRequestRepository.FilterAsync(model, filterConditions, ConsultationRequestMapper.MapAdminSideConsultationRequest);

            var adviserUserIds = model.Entities.Select(x => x.AdviserUserId.Value).ToList();
            var adviserInfo = await _userService.GetUserNameAndUserProfileByUserId(adviserUserIds);
            model.Entities = model.Entities
                .Select(x => x
                .MapFrom(adviserInfo.Value.First(n => n.Id == x.AdviserUserId)))
                .ToList();

            var requestedUserIds = model.Entities.Select(x => x.RequestedByUserId.Value).ToList();
            var requestedUserInfo = await _userService.GetUserNameAndUserProfileByUserId(requestedUserIds);
            model.Entities = model.Entities
                .Select(x => x
                .MapFromUserId(requestedUserInfo.Value.First(n => n.Id == x.RequestedByUserId)))
                .ToList();
            return model;
        }
        public async Task<Result<ClientSideFilterConsultationRequestViewModel>> ClientSideFilterAsync(ClientSideFilterConsultationRequestViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterConsultationRequestViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<ConsultationRequest>();


            await _consultationRequestRepository.FilterAsync(model, filterConditions, ConsultationRequestMapper.MapClientSideConsultationRequest);

            var adviserUserIds = model.Entities.Select(x => x.AdviserUserId.Value).ToList();
            var adviserInfo = await _userService.GetUserNameAndUserProfileByUserId(adviserUserIds);
            model.Entities = model.Entities
                .Select(x => x
                .MapFrom(adviserInfo.Value.First(n => n.Id == x.AdviserUserId)))
                .ToList();

            return model;
        }

        #endregion

        #region Create
        public async Task<Result<int>> CreateAsync(UpsertConsultationRequestViewModel model)
        {
            #region Validations
            if (model is null) return Result.Failure<int>(ErrorMessages.NullValue);
            if (model.AdviserConsultationTypeId is null) return Result.Failure<int>(ErrorMessages.InsertAdviserConsultationTypes);
            if (model.SelectedDateId is null) return Result.Failure<int>(ErrorMessages.InsertAdviserAvailableDates);
            if (model.AdviserId is null) return Result.Failure<int>(ErrorMessages.SelectAdviserError);

            #endregion

            var selectedDate = await _adviserAvailableDateRepository.GetByIdAsync(model.SelectedDateId.Value);
            var selectedType = await _adviserConsultationTypeRespositry.GetByIdAsync(model.AdviserConsultationTypeId.Value);

            var consultationRequest = new ConsultationRequest()
            {
                SelectedDateId = model.SelectedDateId.Value,
                AdviserConsultationTypeId = model.AdviserConsultationTypeId.Value,
                Topic = model.Topic,
                Description = model.Description,
                RequestedByUserId = model.RequestedByUserId.Value,
                AdviserId = model.AdviserId.Value,
                State = ConsultationRequestState.WaitingForPayment,
            };

            await _consultationRequestRepository.InsertAsync(consultationRequest);
            await _consultationRequestRepository.SaveAsync();

            ConsultationRequestCreatedEvent consultationRequestCreatedEvent = new(
            consultationRequest.SelectedDateId,
            consultationRequest.AdviserConsultationTypeId,
            consultationRequest.Topic,
            consultationRequest.Description,
            consultationRequest.RequestedByUserId,
            consultationRequest.AdviserId,
            consultationRequest.State);

            await _mediatorHandler.PublishEvent(consultationRequestCreatedEvent);


            return Result.Success(consultationRequest.Id);

        }
        #endregion

        #region Edit
        public async Task<Result> SetTimeAsync(AdminSideSetTimeForConsultationRequestViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetByIdAsync(model.Id);

            if (request is null) return Result.Failure(ErrorMessages.NullValue);

            var fixedDate = model.SetFixedDate.ConvertToEnglishNumber().ParseUserDateToUTC();

            var newDate = new DateTime(model.SetFixedTime.Ticks).ToUniversalTime();
            fixedDate = fixedDate.Value.Date + newDate.TimeOfDay;

            request.FixedDate = fixedDate.Value.AddDays(1);
            request.State = ConsultationRequestState.WaitingForEvent;

            _consultationRequestRepository.Update(request);

            await _consultationRequestRepository.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #region Get

        public async Task<Result<ClientSideConsultaionRequestViewModel>> ClientSideGetConsultaionRequest(int id)
        {
            if (id == null || id <= 0) return Result.Failure<ClientSideConsultaionRequestViewModel>(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetConsultationRequestWithDateAndTypeAsync(id);

            var requestViewModel = new ClientSideConsultaionRequestViewModel().MapFrom(request);

            var userIds = new List<int>();
            userIds.Add(request.Adviser.UserId);
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            if (usersInfo.IsSuccess)
                requestViewModel.AdviserUserName = usersInfo.Value.FirstOrDefault().UserName;

            return Result.Success(requestViewModel);
        }


        public async Task<Result<AdminSideConsultaionRequestViewModel>> AdminSideGetConsultaionRequestForSetTime(int id)
        {
            if (id == null || id <= 0) return Result.Failure<AdminSideConsultaionRequestViewModel>(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetConsultationRequestWithDateAndTypeAsync(id);

            var requestViewModel = new AdminSideConsultaionRequestViewModel().MapFrom(request);


            var adviserIds = new List<int>();
            adviserIds.Add(request.Adviser.UserId);
            var advisersInfo = await _userService.GetUserNameAndUserProfileByUserId(adviserIds);
            if (advisersInfo.IsSuccess)
                requestViewModel.AdviserUserName = advisersInfo.Value.FirstOrDefault().UserName;
                requestViewModel.AdviserMobile = advisersInfo.Value.FirstOrDefault().Mobile;

            var userIds = new List<int>();
            userIds.Add(request.RequestedByUserId);
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            if (usersInfo.IsSuccess)
                requestViewModel.RequestedByUserName = usersInfo.Value.FirstOrDefault().UserName;
                requestViewModel.RequestedByUserMobile = usersInfo.Value.FirstOrDefault().Mobile;

      

            return Result.Success(requestViewModel);

        }

        #endregion

        #region Cancele
        public async Task<Result> CanceleRequestByAdmin(int id)
        {
            if (id == null || id <= 0) return Result.Failure(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetConsultationRequestWithDateAndTypeAsync(id);

            if (request == null) return Result.Failure(ErrorMessages.NullValue);

            if (request.State == ConsultationRequestState.WaitingForEvent || request.State == ConsultationRequestState.WaitingForSetTime)
            {
                var walletTransaction = new WalletTransactionDetailsViewModel()
                {
                    TransactionType = WalletTransactionType.Withdraw,
                    UserId = request.RequestedByUserId,
                    ConsulationRequestId = request.Id,
                };
                var transactionToAdviser = await _walletService.GetWalletTransactionByuserIdAsync(walletTransaction);

                if (transactionToAdviser.IsFailure)
                    return Result.Failure(transactionToAdviser.Message);

                var charge = new AdminSideChargeWalletForConsultationRequestViewModel()
                {
                    UserId = request.RequestedByUserId,
                    Amount = request.AdviserConsultationType.Price,
                    ConsultationRqeeuestId = request.Id,
                    UserIP = transactionToAdviser.Value.IP,
                };
                var chargeResult = await _walletService.CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(charge);

                if (chargeResult.IsFailure)
                    return Result.Failure(chargeResult.Message);
            }
            else if (request.State == ConsultationRequestState.Finished)
            {
                var adviser = await _adviserSerivce.GetAdviserAsync(request.AdviserId);
                if (adviser.IsFailure)
                    return Result.Failure(ErrorMessages.NullValue);

                var walletTransaction = new WalletTransactionDetailsViewModel()
                {
                    TransactionType = WalletTransactionType.Deposit,
                    UserId = adviser.Value.UserId,
                    ConsulationRequestId = request.Id,
                };
               
                var transactionToUser = await _walletService.GetWalletTransactionByuserIdAsync(walletTransaction);

                if (transactionToUser.IsFailure)
                    return Result.Failure(transactionToUser.Message);

                var withrawFromAdviser = new WithdrawFromWalletViewModel()
                {
                    UserId = adviser.Value.UserId,
                    ConsultationRequestId = request.Id,
                    Amount = transactionToUser.Value.Amount,
                    UserIP = transactionToUser.Value.IP,
                };

                var withrawResult = await _walletService.WithdrawFromWalletForReturnConsultationRequestMoneyAsync(withrawFromAdviser);

                if (transactionToUser.IsFailure)
                    return Result.Failure(transactionToUser.Message);

                var charge = new AdminSideChargeWalletForConsultationRequestViewModel()
                {
                    UserId = request.RequestedByUserId,
                    Amount = request.AdviserConsultationType.Price,
                    ConsultationRqeeuestId = request.Id,
                    UserIP = transactionToUser.Value.IP,
                };
                var chargeResult = await _walletService.CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(charge);

                if (chargeResult.IsFailure)
                    return Result.Failure(chargeResult.Message);
            }
            request.State = ConsultationRequestState.Canceled;

            _consultationRequestRepository.Update(request);
            await _consultationRequestRepository.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> CanceleRequestByUser(int id)
        {
            if (id == null || id <= 0) return Result.Failure(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetConsultationRequestWithDateAndTypeAsync(id);

            if (request == null) return Result.Failure(ErrorMessages.NullValue);

            if (request.State == ConsultationRequestState.WaitingForSetTime || request.State == ConsultationRequestState.WaitingForPayment)
            {
                var walletTransaction = new WalletTransactionDetailsViewModel()
                {
                    TransactionType = WalletTransactionType.Withdraw,
                    UserId = request.RequestedByUserId,
                    ConsulationRequestId = request.Id,
                };

                var transactionToUser = await _walletService.GetWalletTransactionByuserIdAsync(walletTransaction);

                if (transactionToUser.IsFailure)
                    return Result.Failure(transactionToUser.Message);

                var charge = new AdminSideChargeWalletForConsultationRequestViewModel()
                {
                    UserId = request.RequestedByUserId,
                    Amount = request.AdviserConsultationType.Price,
                    ConsultationRqeeuestId = request.Id,
                    UserIP = transactionToUser.Value.IP,
                };
                var chargeResult = await _walletService.CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(charge);

                if (chargeResult.IsFailure)
                    return Result.Failure(chargeResult.Message);
                request.State = ConsultationRequestState.Canceled;

                _consultationRequestRepository.Update(request);
                await _consultationRequestRepository.SaveAsync();
                return Result.Success();
            }

            return Result.Failure(ErrorMessages.NullValue);

        }

        #endregion

        #region Finished

        public async Task<Result> FinishedRequestByAdmin(int id)
        {
            if (id == null || id <= 0) return Result.Failure(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetConsultationRequestWithDateAndTypeAsync(id);

            if (request == null) return Result.Failure(ErrorMessages.NullValue);

            var adviser = await _adviserSerivce.GetAdviserAsync(request.AdviserId);

            if (adviser.IsFailure) return Result.Failure(ErrorMessages.NullValue);

            var teacherIncome = (request.AdviserConsultationType.Price * (adviser.Value.ConsultationPercentage / (decimal)100));

            var charge = new AdminSideChargeWalletForConsultationRequestViewModel()
            {
                UserId = adviser.Value.UserId,
                Amount = teacherIncome,
                ConsultationRqeeuestId = request.Id,
                UserIP = " ",
            };
            var chargeResult = await _walletService.CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(charge);

            if (chargeResult.IsFailure)
                return Result.Failure(chargeResult.Message);

            request.State = ConsultationRequestState.Finished;

            _consultationRequestRepository.Update(request);
            await _consultationRequestRepository.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #region Buy
        public async Task<Result<WalletTransactionDetailsViewModel>> BuyConsultationRequestAsync(ClientSideBuyConsultationRequestViewModel model)
        {
            var amount = await GetAmountById(model.ConsultationRequestId);
            var balance = await _walletService.GetBalanceAsync(model.UserId.Value);

            if (!model.UseFromWalletBalnace || balance.Value < amount.Value)
            {
                if (model.UseFromWalletBalnace)
                {
                    amount = amount.Value - balance.Value;
                }

                ClientSideChargeWalletForConsultationRequestViewModel clientSideChargeWalletForConsultation = new()
                {
                    Amount = amount.Value,
                    UserId = model.UserId.Value,
                    UserIP = model.UserIP,
                    ConsultationRqeeuestId = model.ConsultationRequestId
                };

                return await _walletService.CreateChargeWalletTransactionForBuyConsultationRequestAsync(clientSideChargeWalletForConsultation);
            }
            else
            {
                ClientSideConfirmConsultationRequestViewModel confirmConsultationRequestViewModel = new()
                {
                    UserId = model.UserId.Value,
                    ConsultationRequestId = model.ConsultationRequestId,
                    UserIp = model.UserIP
                };

                var result = await ConfirmConsultationRequestForUser(confirmConsultationRequestViewModel);



                return result;
            }




        }

        #endregion

        #region Confirm Purchase

        public async Task<Result<WalletTransactionDetailsViewModel>> ConfirmConsultationRequestForUser(ClientSideConfirmConsultationRequestViewModel model)
        {
            var amount = await GetAmountById(model.ConsultationRequestId);
            var balance = await _walletService.GetBalanceAsync(model.UserId);

            if (amount.Value <= balance.Value)
            {
                WithdrawFromWalletViewModel withdrawFromWalletViewModel = new()
                {
                    Amount = amount.Value,
                    UserId = model.UserId,
                    ConsultationRequestId = model.ConsultationRequestId,
                    UserIP = model.UserIp

                };

                var withdrawFromWallet = await _walletService.WithdrawFromWalletForBuyConsultationRequestAsync(withdrawFromWalletViewModel);

                await ChangeConsultationRequestState(model.ConsultationRequestId, ConsultationRequestState.WaitingForSetTime);

                return withdrawFromWallet;

            }
            else
            {
                return Result.Failure<WalletTransactionDetailsViewModel>(ErrorMessages.FailedOperationError);
            }
        }
        #endregion

        #region Get Aomunt
        public async Task<Result<decimal>> GetAmountById(int id)
        {
            if (id == null || id <= 0) return Result.Failure<decimal>(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetByIdAsync(id, includeProperties: nameof(ConsultationRequest.AdviserConsultationType));

            if (request is null) return Result.Failure<decimal>(ErrorMessages.NullValue);

            return request.AdviserConsultationType.Price;
        }


        #endregion

        #region Utilities

        public async Task<Result> ChangeConsultationRequestState(int requestId, ConsultationRequestState state)
        {
            if (requestId == null || requestId <= 0) return Result.Failure<decimal>(ErrorMessages.NullValue);

            var request = await _consultationRequestRepository.GetByIdAsync(requestId);

            if (request is null) return Result.Failure<decimal>(ErrorMessages.NullValue);

            request.State = state;

            _consultationRequestRepository.Update(request);
            await _consultationRequestRepository.SaveAsync();

            return Result.Success();
        }

        #endregion

        #endregion



    }
}