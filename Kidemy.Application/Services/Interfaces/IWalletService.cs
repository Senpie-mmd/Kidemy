using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IWalletService
    {
        Task<Result> ConfirmPaidTransactionAsync(int id, string refId);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionAsync(ChargeWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionAsync(ClientSideChargeWalletViewModel model);
        Task<Result<FilterWalletTransactionViewModel>> FilterAsync(FilterWalletTransactionViewModel model);
        Task<Result<decimal>> GetBalanceAsync(int userId, bool applyBlockedAmount = false);
        Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByIdAsync(int id);
        Task<Result<decimal>> GetWalletTransactionAmountAsync(int id);
        Task<Result> SetReferenceIdForTransaction(int id, string refId);
        Task<Result> ChargeWalletByAdminAsync(ChargeWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletByAdminAsync(WithdrawFromWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByOrderIdAsync(int orderId);
        Task<Result<WalletTransactionDetailsViewModel>> GetWalletTransactionByuserIdAsync(WalletTransactionDetailsViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForOrderPaymentAsync(WithdrawFromWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForPayOrderAsync(ChargeWalletForOrderViewModel model);
		Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletByUserWithdrawRequestAsync(WithdrawFromWalletViewModel model);
		Task<Result<WalletTransactionDetailsViewModel>> UndoWalletTransaction(int id);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForBuyVIPPlanAsync(ClientSideChargeWalletForVIPPlanViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForBuyVIPPlanAsync(WithdrawFromWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForBuyConsultationRequestAsync(ClientSideChargeWalletForConsultationRequestViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForReturnConsultationRequestMoneyAsync(AdminSideChargeWalletForConsultationRequestViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForBuyConsultationRequestAsync(WithdrawFromWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> WithdrawFromWalletForReturnConsultationRequestMoneyAsync(WithdrawFromWalletViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> CreateChargeWalletTransactionForCourseCommissionAsync(ChargeWalletForCommissionViewModel model);
    }
}
