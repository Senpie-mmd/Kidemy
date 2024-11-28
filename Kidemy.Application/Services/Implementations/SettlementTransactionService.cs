using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Domain.Events.SettlementTransaction;
using Kidemy.Domain.Interfaces.Wallet;
using Kidemy.Domain.Models.SettlementTransaction;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations;

public class SettlementTransactionService : ISettlementTransactionService
{
    #region Fields

    private readonly ISettlementTransactionRepository _settlementTransactionRepository;
    private readonly IMediatorHandler _mediatorHandler;

    #endregion

    #region Constructor

    public SettlementTransactionService(ISettlementTransactionRepository settlementTransactionRepository, IMediatorHandler mediatorHandler)
    {
        _settlementTransactionRepository = settlementTransactionRepository;
        _mediatorHandler = mediatorHandler;
    }

    #endregion

    #region Methods

    #region Create
    public async Task<Result> CreateSettlementTransactionAsync(AdminSideUpsertSettlementTransactionViewModel model)
    {
        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        var settlementTransaction = new SettlementTransaction
        {
            CreatedDateOnUtc = DateTime.UtcNow,
            UpdatedDateOnUtc = DateTime.UtcNow,
            TransactionDate = model.TransactionDate.ConvertToEnglishNumber().ParseUserDateToUTC().Value,
            Price = (decimal)model.Price,
            CardNumber = model.CardNumber,
            TrackingCode = model.TrackingCode,
            AccountNumber = model.AccountNumber,
            UserId = model.UserId
        };
        await _settlementTransactionRepository.InsertAsync(settlementTransaction);
        await _settlementTransactionRepository.SaveAsync();


        SettlementTransactionCreatedEvent settlementTransactionCreatedEvent = new(
               settlementTransaction.Price,
               settlementTransaction.CardNumber,
               settlementTransaction.TrackingCode,
               settlementTransaction.AccountNumber,
               settlementTransaction.UserId);

        await _mediatorHandler.PublishEvent(settlementTransactionCreatedEvent);


        return Result.Success();
    }
    #endregion

    #region Filter

    public async Task<Result<AdminSideFilterSettlementTransactionsViewModel>> FilterSettlementTransactionsAsync(AdminSideFilterSettlementTransactionsViewModel model)
    {
        if (model is null) return Result.Failure<AdminSideFilterSettlementTransactionsViewModel>(ErrorMessages.NullValue);

        var filterCondition = Filter.GenerateConditions<SettlementTransaction>();

        filterCondition.Add(d => !d.IsDelete);

        if (model.AccountNumber is not null)
            filterCondition.Add(d => d.AccountNumber.Contains(model.AccountNumber));

        if (model.CardNumber is not null)
            filterCondition.Add(d => d.CardNumber.Contains(model.CardNumber));

        if (model.UserId is not null)
            filterCondition.Add(d => d.UserId == model.UserId);

        await _settlementTransactionRepository.FilterAsync(model, filterCondition, SettlementTransactionMapper.MapAdminSideSettlementTransactionViewModel, orderByDesc: ac => ac.UpdatedDateOnUtc);

        return model;
    }
    #endregion

    #region Get

    public async Task<Result<AdminSideSettlementTransactionViewModel>> GetSettlementTransactionAsync(int id)
    {
        var settlementTransaction = await _settlementTransactionRepository.GetByIdAsync(id);

        if (settlementTransaction is null) return Result.Failure<AdminSideSettlementTransactionViewModel>(ErrorMessages.NullValue);


        var model = new AdminSideSettlementTransactionViewModel().MapFrom(settlementTransaction);

        return Result.Success(model);
    }

    public async Task<Result<AdminSideUpsertSettlementTransactionViewModel>> GetSettlementTransactionForEditAsync(int id)
    {
        var settlementTransaction = await _settlementTransactionRepository.GetByIdAsync(id);

        if (settlementTransaction is null) return Result.Failure<AdminSideUpsertSettlementTransactionViewModel>(ErrorMessages.NullValue);

        var model = new AdminSideUpsertSettlementTransactionViewModel().MapFrom(settlementTransaction);

        return Result.Success(model);
    }

    public async Task<Result<List<AdminSideSettlementTransactionViewModel>>> GetAllAsync()
    {
        var entities = await _settlementTransactionRepository.GetAllAsync(d => !d.IsDelete);

        var models = entities.Select(settlementTransaction => new AdminSideSettlementTransactionViewModel().MapFrom(settlementTransaction)).ToList()
        ?? new List<AdminSideSettlementTransactionViewModel>();

        return Result.Success(models);
    }

    public async Task<Result<ClientSideFilterSettlementTransactionViewModel>> ClientSideFilterSettlementTransactionsAsync(ClientSideFilterSettlementTransactionViewModel model)
    {
        if (model is null) return Result.Failure<ClientSideFilterSettlementTransactionViewModel>(ErrorMessages.NullValue);

        var filterCondition = Filter.GenerateConditions<SettlementTransaction>();

        filterCondition.Add(d => !d.IsDelete);

        if (model.AccountNumber is not null)
            filterCondition.Add(d => d.AccountNumber.Contains(model.AccountNumber));

        if (model.CardNumber is not null)
            filterCondition.Add(d => d.CardNumber.Contains(model.CardNumber));

        if (model.UserId is not null)
            filterCondition.Add(d => d.UserId == model.UserId);

        await _settlementTransactionRepository.FilterAsync(model, filterCondition, SettlementTransactionMapper.MapClientSideSettlementTransactionViewModel, orderByDesc: ac => ac.Id);


        return model;
    }

    #endregion

    #region Update

    public async Task<Result> EditSettlementTransactionAsync(AdminSideUpsertSettlementTransactionViewModel model)
    {
        var settlementTransaction = await _settlementTransactionRepository.GetByIdAsync(model.Id);

        if (settlementTransaction is null) return Result.Failure(ErrorMessages.NullValue);

        var transactionDate = model.TransactionDate.ConvertToEnglishNumber().ParseUserDateToUTC();

        if (transactionDate is null)
            return Result.Failure(ErrorMessages.TransactionDateIsRequired);

        SettlementTransactionUpdatedEvent settlementTransactionCreatedEvent = new(
            settlementTransaction.Price,
            model.Price,
            settlementTransaction.CardNumber,
            model.CardNumber,
            settlementTransaction.TrackingCode,
            model.TrackingCode,
            settlementTransaction.AccountNumber,
            model.AccountNumber,
            settlementTransaction.UserId,
            model.UserId
            );

        await _mediatorHandler.PublishEvent(settlementTransactionCreatedEvent);

        settlementTransaction.MapFrom(model);

        _settlementTransactionRepository.Update(settlementTransaction);

        await _settlementTransactionRepository.SaveAsync();



        return Result.Success();
    }
    #endregion

    #region Delete
    public async Task<Result> DeleteSettlementTransactionAsync(int id)
    {
        var SettlementTransaction = await _settlementTransactionRepository.GetByIdAsync(id);

        if (SettlementTransaction is null) return Result.Failure(ErrorMessages.NullValue);

        SettlementTransaction.IsDelete = true;

        _settlementTransactionRepository.Update(SettlementTransaction);
        await _settlementTransactionRepository.SaveAsync();

        SettlementTransactionDeletedEvent settlementTransactionDeletedEvent = new(id);

        await _mediatorHandler.PublishEvent(settlementTransactionDeletedEvent);


        return Result.Success();
    }

    #endregion

    #endregion 
}