using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Enums.Sms;

namespace Kidemy.Domain.Interfaces.SmsProvider
{
    public interface ISmsProviderRepository : IRepository<Models.SmsProvider.SmsProvider, int>
    {
        Task<Models.SmsProvider.SmsProvider> GetDefaultSmsProvider();
        Task<SmsProviderType> GetDefaultSmsProviderType();
        Task<Models.SmsProvider.SmsProvider> GetSmsProviderType(SmsProviderType smsProviderType);
    }
}
