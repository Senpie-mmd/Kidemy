using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PARSGREEN.RESTful.SMS;

namespace Kidemy.Application.Services.Implementations
{
    public class SmsSenderService : ISmsSenderSevice
    {
        private readonly ILogger<SmsSenderService> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly ISmsProviderService _smsProviderService;

        public SmsSenderService(ILogger<SmsSenderService> logger, IStringLocalizer localizer, ISmsProviderService smsProviderService)
        {
            _logger = logger;
            _localizer = localizer;
            _smsProviderService = smsProviderService;
        }


        public async Task<Result> SendSms(string[] mobiles, string textMessage)
        {
            var result = await _smsProviderService.GetDefaultSmsProviderType();

            if (result.IsFailure)
            {
                _logger.LogError("Get default sms provider failed. message: " + result.Message);
                return Result.Failure(result.Message);
            }

            try
            {
                switch (result.Value)
                {
                    case SmsProviderType.ParsGreen:
                        ParsGreenSendSms(mobiles, textMessage);
                        break;
                    default:
                        break;
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"sms sending failed.");
                return Result.Failure(ErrorMessages.SmsDidNotSendError);
            }
        }

        private void ParsGreenSendSms(string[] mobiles, string textMessage)
        {
            var message = new Message("6AC5E403-C244-49B9-A497-E0436649FBBB");
            var parsGreenResult = message.SendSms(
                $"{_localizer[SiteTools.SiteName].ToString()}{Environment.NewLine}{textMessage}", mobiles);

            if (parsGreenResult is null || !parsGreenResult.R_Success)
            {
                _logger.LogWarning($"sms sending failed. ParsGreen response: {JsonConvert.SerializeObject(parsGreenResult)}");
                throw new Exception();
            }
        }

        public Task<Result> SendSms(string mobile, string textMessage)
        {
            return SendSms(new[] { mobile }, textMessage);
        }
    }
}
