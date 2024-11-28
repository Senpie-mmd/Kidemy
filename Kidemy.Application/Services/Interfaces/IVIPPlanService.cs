using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IVIPPlanService
    {
        Task<Result<List<ClientSideVIPPlanViewModel>>> GetAllVIPPlans();
        Task<Result<List<AdminSideVIPPlanViewModel>>> GetAllVIPPlansForAdmin();
        Task<Result<decimal>> GetAmountById(int id);
        Task<Result<WalletTransactionDetailsViewModel>> BuyVIPPlanAsync(ClientSideBuyVIPPlanViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> ConfirmPlanForUser(ClientSideConfirmPlanViewModel model);
        Task<Result> CreateAsync(AdminSideUpsertVIPPlanViewModel model);
        Task<Result<AdminSideFilterVIPPlanViewModel>> FilterAsync(AdminSideFilterVIPPlanViewModel model);
        Task<Result<AdminSideUpsertVIPPlanViewModel>> GetVIPPlanForEditByIdAsync(int id);
        Task<Result> UpdateAsync(AdminSideUpsertVIPPlanViewModel model);
        Task<Result> DeleteAsync(int id);
        Task<Result> AssignPlanForUserByAdminAsync(int PlanId, int UserId);
    }
}
