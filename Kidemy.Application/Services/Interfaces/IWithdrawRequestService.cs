using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Models.WithdrawRequest;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IWithdrawRequestService
    {
        Task<Result> AcceptAsync(AcceptWithdrawRequestViewModel model);
        Task<Result> CreateAsync(UpsertWithdrawRequestViewModel model);
        Task<Result<FilterWithdrawRequestViewModel>> FilterWithdrawRequestAsync(FilterWithdrawRequestViewModel model);
        Task<Result> RejectAsync(RejectWithdrawRequestViewModel model);
        Task<Result<int>> GetWithdrawRequestCountAsync(WithdrawRequestStatus status);
    }
}
