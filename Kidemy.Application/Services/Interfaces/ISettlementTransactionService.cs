using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces;

public interface ISettlementTransactionService
{
    Task<Result> CreateSettlementTransactionAsync(AdminSideUpsertSettlementTransactionViewModel model);

    Task<Result<AdminSideFilterSettlementTransactionsViewModel>> FilterSettlementTransactionsAsync(AdminSideFilterSettlementTransactionsViewModel model);

    Task<Result<AdminSideSettlementTransactionViewModel>> GetSettlementTransactionAsync(int id);

    Task<Result<AdminSideUpsertSettlementTransactionViewModel>> GetSettlementTransactionForEditAsync(int id);

    Task<Result> EditSettlementTransactionAsync(AdminSideUpsertSettlementTransactionViewModel model);

    Task<Result> DeleteSettlementTransactionAsync(int id);

    Task<Result<List<AdminSideSettlementTransactionViewModel>>> GetAllAsync();

    Task<Result<ClientSideFilterSettlementTransactionViewModel>> ClientSideFilterSettlementTransactionsAsync(ClientSideFilterSettlementTransactionViewModel filter);

}