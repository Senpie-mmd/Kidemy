using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Domain.Enums.BankAccount;
using Kidemy.Domain.Events.BankAccountCard;
using Kidemy.Domain.Interfaces.BankAccountCard;
using Kidemy.Domain.Models.BankAccountCard;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementation
{
    public class BankAccountCardService : IBankAccountCardService
    {
        #region Fields

        private readonly IBankAccountCardRepository _bankAccountCardRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BankAccountCardService> _logger;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public BankAccountCardService(IBankAccountCardRepository bankAccountCardRepository, ILogger<BankAccountCardService> logger, IUserService userService, IHttpContextAccessor httpContextAccessor, IMediatorHandler mediatorHandler)
        {
            _bankAccountCardRepository = bankAccountCardRepository;
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result> CreateAsync(UpsertBankAccountCardViewModel model)
        {
            if (model == null)
            {
                _logger.LogError("BankAccountCardService: AddBankAccountCardAsync: model is null");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }



            var duplicatedAccounts = await _bankAccountCardRepository.GetAllAsync(b =>
                                                            b.CardNumber == model.CardNumber ||
                                                            b.AccountNumber == model.AccountNumber ||
                                                            b.ShabaNumber == model.ShabaNumber);

            if (duplicatedAccounts?.Any() ?? false)
            {
                return Result.Failure(ErrorMessages.BankAccountCardAlreadyExistsByTheseInfoError);
            }

            var bankAccountCard = new BankAccountCard
            {
                BankName = model.BankName,
                CardNumber = model.CardNumber.Trim() ?? string.Empty,
                ShabaNumber = model.ShabaNumber.Trim() ?? string.Empty,
                AccountNumber = model.AccountNumber.Trim() ?? string.Empty,
                UserId = _httpContextAccessor.HttpContext.User.GetUserId(),
                Status = BankAccountCardStatus.Pending,
                OwnerName=model.OwnerName
            };

            await _bankAccountCardRepository.InsertAsync(bankAccountCard);
            await _bankAccountCardRepository.SaveAsync();

            BankAccountCardCreatedEvent bankAccountCardCreatedEvent = new(
            model.BankName,
            model.CardNumber,
            model.ShabaNumber,
            model.AccountNumber,
            model.UserId,
            model.Status,
            model.OwnerName
            );

            await _mediatorHandler.PublishEvent(bankAccountCardCreatedEvent);

            return Result.Success();
        }

        public async Task<Result> DeleteBankAccountCardAsync(int id)
        {
            if (id != null && id <= 0)
                return Result.Failure(ErrorMessages.ProcessFailedError);

            await _bankAccountCardRepository.SoftDelete(id);
            await _bankAccountCardRepository.SaveAsync();


            BankAccountCardDeletedEvent bankAccountCardDeletedEvent = new(
         id);
            await _mediatorHandler.PublishEvent(bankAccountCardDeletedEvent);

            return Result.Success();
        }

        public async Task<Result> SetDefultBankAccountCardAsync(int id)
        {


            var expectedBankAccountCard = await _bankAccountCardRepository.GetByIdAsync(id);
            if (expectedBankAccountCard is null) return Result.Failure(ErrorMessages.ProcessFailedError);


            expectedBankAccountCard.IsDefault = true;
            expectedBankAccountCard.UpdatedDateOnUtc = DateTime.UtcNow;

            var lastDefaultAccount = await _bankAccountCardRepository.FirstOrDefaultAsync(x => x.UserId == expectedBankAccountCard.UserId && x.Id != id);

            if (lastDefaultAccount != null)
            {
                lastDefaultAccount.IsDefault = false;
                lastDefaultAccount.UpdatedDateOnUtc = DateTime.UtcNow;
                _bankAccountCardRepository.Update(lastDefaultAccount);
            }
            
            BankAccountCardSetDefaultEvent bankAccountCardSetDefaultEvent = new(
               lastDefaultAccount != null ? lastDefaultAccount.Id : 0 ,
             expectedBankAccountCard.Id
             );

            await _mediatorHandler.PublishEvent(bankAccountCardSetDefaultEvent);

            await _bankAccountCardRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<FilterBankAccountCardViewModel>> FilterBankAccountCardAsync(FilterBankAccountCardViewModel model)
        {
            if (model is null)
            {
                return Result.Failure<FilterBankAccountCardViewModel>(ErrorMessages.ProcessFailedError);
            }

            var conditions = Filter.GenerateConditions<BankAccountCard>();

            /*if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                conditions.Add(x => x.User.FirstName.Contains(model.UserName) || x.User.LastName.Contains(model.UserName) || x.User.Mobile.Contains(model.UserName));
            }*/

            if (!string.IsNullOrWhiteSpace(model.CardNumber))
            {
                conditions.Add(x => x.CardNumber == model.CardNumber);
            }

            if (!string.IsNullOrWhiteSpace(model.ShabaNumber))
            {
                conditions.Add(x => x.ShabaNumber == model.ShabaNumber);
            }

            if (!string.IsNullOrWhiteSpace(model.AccountNumber))
            {
                conditions.Add(x => x.AccountNumber == model.AccountNumber);
            }

            if (model.Status != null && model.Status != BankAccountCardStatus.All)
            {
                conditions.Add(x => x.Status == model.Status);
            }

            if (model.IsDefault != null)
            {
                conditions.Add(x => x.IsDefault == model.IsDefault);
            }

            if (model.UserId is not null)
            {
                conditions.Add(x => x.UserId == model.UserId);
            }

            await _bankAccountCardRepository.FilterAsync(model, conditions, BankAccountCardMapper.MapAdminSideBankAccountCardsViewModel, orderBy: ac => ac.CreatedDateOnUtc);

            var userIds = model.Entities.Select(n => n.UserId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(card => card
                .MapFrom(usersInfo.Value.First(n => n.Id == card.UserId)))
                .ToList();

            return model;
        }

        public async Task<Result<BankAccountCardViewModel>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Failure<BankAccountCardViewModel>(ErrorMessages.ProcessFailedError);
            }

            var bankAccountCard = await _bankAccountCardRepository.GetByIdAsync(id);

            if (bankAccountCard is null)
            {
                _logger.LogError("BankAccountCardService: GetByIdAsync: Not found by id: " + id);
                return Result.Failure<BankAccountCardViewModel>(ErrorMessages.ProcessFailedError);
            }

            return new BankAccountCardViewModel().MapFrom(bankAccountCard);
        }

        public async Task<Result> ChangeStatusAsync(ChangeStatusBankAccountCardViewModel model)
        {
            if (model is null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var bankAccountCard = await _bankAccountCardRepository.GetByIdAsync(model.Id);

            if (bankAccountCard == null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            bankAccountCard.Status = model.Status;
            bankAccountCard.Description = model.Description;
            bankAccountCard.UpdatedDateOnUtc = DateTime.UtcNow;

            _bankAccountCardRepository.Update(bankAccountCard);
            await _bankAccountCardRepository.SaveAsync();

            BankAccountCardChangeStatusEvent bankAccountCardChangeStatusEvent = new(
            bankAccountCard.Status,
            model.Status
         );

            await _mediatorHandler.PublishEvent(bankAccountCardChangeStatusEvent);

            return Result.Success();
        }

        public async Task<Result> ChangeStatusAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var bankAccountCard = await _bankAccountCardRepository.GetByIdAsync(id);

            if (bankAccountCard == null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            BankAccountCardChangeStatusEvent bankAccountCardChangeStatusEvent = new(
             bankAccountCard.Status,
             BankAccountCardStatus.Accepeted
          );

            await _mediatorHandler.PublishEvent(bankAccountCardChangeStatusEvent);

            bankAccountCard.Status = BankAccountCardStatus.Accepeted;
            bankAccountCard.UpdatedDateOnUtc = DateTime.UtcNow;

            _bankAccountCardRepository.Update(bankAccountCard);
            await _bankAccountCardRepository.SaveAsync();

            return Result.Success();
        }

        #endregion
    }
}
