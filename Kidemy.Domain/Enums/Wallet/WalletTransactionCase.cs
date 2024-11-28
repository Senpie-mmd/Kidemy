using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Wallet
{
    public enum WalletTransactionCase
    {
        [Display(Name = "ChargeWallet")]
        ChargeWallet,

        [Display(Name = "PayOrder")]
        PayOrder,

        [Display(Name = "AdminActivity")]
        AdminActivity,

        [Display(Name = "WithdrawRequest")]
        WithdrawRequest,

        [Display(Name = "UndoTransaction")]
        UndoTransaction,

        [Display(Name = "VIPPlan")]
        VIPPlan,
        [Display(Name = "ConsultationRequest")]
        ConsultationRequest,
        [Display(Name = "ReturnConsultationRequestMoney")]
        ReturnConsultationRequestMoney,

        [Display(Name = "CourseCommission")]
        CourseCommission,
    }
}
