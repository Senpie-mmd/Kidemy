using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IBankAccountCardService
    {
        Task<Result> ChangeStatusAsync(ChangeStatusBankAccountCardViewModel model);
        Task<Result> ChangeStatusAsync(int id);
        Task<Result> CreateAsync(UpsertBankAccountCardViewModel model);
        Task<Result> DeleteBankAccountCardAsync(int id);
        Task<Result<FilterBankAccountCardViewModel>> FilterBankAccountCardAsync(FilterBankAccountCardViewModel model);
        Task<Result<BankAccountCardViewModel>> GetByIdAsync(int id);
        Task<Result> SetDefultBankAccountCardAsync(int id);
    }
}
