using Kidemy.Application.ViewModels.SmsProvider;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISmsProviderService
    {
        Task<Result> UpdateSmsProviderAsync(AdminSideUpsertSmsProviderViewModel model);
        Task<Result<AdminSideUpsertSmsProviderViewModel>> GetSmsProviderForEditAsync(int id);
        Task<Result<FilterSmsProviderViewModel>> FilterAsync(FilterSmsProviderViewModel model);
        Task<Result<SmsProviderType>> GetDefaultSmsProviderType();
        Task<AdminSideUpsertSmsProviderViewModel> GetDefaultSmsProvider();
        Task<AdminSideUpsertSmsProviderViewModel> GetSmsProviderType(SmsProviderType smsProviderType);

    }
}
