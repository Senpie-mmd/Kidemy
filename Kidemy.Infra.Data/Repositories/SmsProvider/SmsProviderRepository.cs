using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Interfaces.SmsProvider;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.SmsProvider
{
    public class SmsProviderRepository : EfRepository<Domain.Models.SmsProvider.SmsProvider, int>, ISmsProviderRepository
    {
        public SmsProviderRepository(KidemyContext context) : base(context)
        {

        }

        public Task<Domain.Models.SmsProvider.SmsProvider> GetDefaultSmsProvider()
        {
            return _dbSet.FirstOrDefaultAsync(provider => !provider.IsDeleted && provider.IsDefault);
        }

        public Task<SmsProviderType> GetDefaultSmsProviderType()
        {
            return _dbSet.Where(provider => !provider.IsDeleted && provider.IsDefault)
                               .Select(p => p.SmsProviderType)
                               .FirstOrDefaultAsync();
        }

        public Task<Domain.Models.SmsProvider.SmsProvider> GetSmsProviderType(SmsProviderType smsProviderType)
        {
            return  _dbSet.FirstOrDefaultAsync(provider => provider.SmsProviderType == smsProviderType);
        }
    }
}
