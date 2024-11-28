using Kidemy.Application.ViewModels.SmsProvider;
using Kidemy.Domain.Models.SmsProvider;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class SmsProviderMapper
    {
        public static AdminSideUpsertSmsProviderViewModel MapFrom(this AdminSideUpsertSmsProviderViewModel model, SmsProvider smsProvider)
        {
            model.Id = smsProvider.Id;
            model.ApiKey = smsProvider.ApiKey;
            model.SmsProviderType = smsProvider.SmsProviderType;
            model.IsDefault = smsProvider.IsDefault;

            return model;
        }

        public static SmsProviderDetailsViewModel MapFrom(this SmsProviderDetailsViewModel model, SmsProvider smsProvider)
        {
            model.Id = smsProvider.Id;
            model.ApiKey = smsProvider.ApiKey;
            model.SmsProviderType = smsProvider.SmsProviderType;
            model.IsDefault = smsProvider.IsDefault;

            return model;
        }

        public static Expression<Func<SmsProvider, AdminSideSmsProviderViewModel>> MapSmsProviderViewModel => (SmsProvider smsProvider) =>
              new AdminSideSmsProviderViewModel
              {
                  Id = smsProvider.Id,
                  ApiKey = smsProvider.ApiKey,
                  IsDefault = smsProvider.IsDefault,
                  SmsProviderType = smsProvider.SmsProviderType

              };

        public static AdminSideSmsProviderViewModel MapFrom(this AdminSideSmsProviderViewModel model, SmsProvider smsProvider)
        {
            model.Id = smsProvider.Id;
            model.ApiKey = smsProvider.ApiKey;
            model.SmsProviderType = smsProvider.SmsProviderType;
            model.IsDefault = smsProvider.IsDefault;

            return model;
        }
    }
}





