using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Enums.Consultation;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IConsultationRequestService
    {
        Task<Result<AdminSideFilterConsultationRequestViewModel>> AdminSideFilterAsync(AdminSideFilterConsultationRequestViewModel model);
        Task<Result<ClientSideFilterConsultationRequestViewModel>> ClientSideFilterAsync(ClientSideFilterConsultationRequestViewModel model);
        Task<Result<int>> CreateAsync(UpsertConsultationRequestViewModel model);
        Task<Result> SetTimeAsync(AdminSideSetTimeForConsultationRequestViewModel model);
        Task<Result<ClientSideConsultaionRequestViewModel>> ClientSideGetConsultaionRequest(int id);
        Task<Result<AdminSideConsultaionRequestViewModel>> AdminSideGetConsultaionRequestForSetTime(int id);
        Task<Result> CanceleRequestByAdmin(int id);
        Task<Result> FinishedRequestByAdmin(int id);
        Task<Result> CanceleRequestByUser(int id);
        Task<Result<decimal>> GetAmountById(int id);
        Task<Result<WalletTransactionDetailsViewModel>> BuyConsultationRequestAsync(ClientSideBuyConsultationRequestViewModel model);
        Task<Result<WalletTransactionDetailsViewModel>> ConfirmConsultationRequestForUser(ClientSideConfirmConsultationRequestViewModel model);

        Task<Result> ChangeConsultationRequestState(int requestId, ConsultationRequestState state);
    }
}
