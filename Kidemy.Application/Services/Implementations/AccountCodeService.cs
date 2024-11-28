using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Domain.Interfaces.Account;
using Kidemy.Domain.Models.Account;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using System.Text.RegularExpressions;

namespace Kidemy.Application.Services.Implementations
{
    public class AccountCodeService : IAccountCodeService
    {
        #region Fields

        private readonly IAccountCodeRepository _accountCodeRepository;

        #endregion

        #region Constructor

        public AccountCodeService(IAccountCodeRepository accountCodeRepository)
        {
            _accountCodeRepository = accountCodeRepository;
        }

        #endregion

        #region Methods

        public async Task<Result> ValidateCodeAsync(string receiver, string code)
        {
            if (string.IsNullOrWhiteSpace(receiver) || string.IsNullOrWhiteSpace(code))
            {
                return Result.Failure(ErrorMessages.ConfirmationCodeIsInvalidError);
            }

            var accountCode = await _accountCodeRepository
                    .LastOrDefaultAsync(filter: ac => ac.Receiver == receiver,
                                            orderBy: ac => ac.Id);

            if (accountCode is null || accountCode.Code != code || accountCode.ExpiryDateTimeUtc < DateTime.UtcNow)
            {
                return Result.Failure(ErrorMessages.ConfirmationCodeIsInvalidError);
            }

            accountCode.ExpiryDateTimeUtc = DateTime.UtcNow.AddMinutes(-SiteTools.ExpireSmsCodeInMinutes);

            _accountCodeRepository.Update(accountCode);
            await _accountCodeRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<string>> GenerateAccountCodeAsync(string receiver)
        {
            var notExpiredAccountCode = await _accountCodeRepository
                    .LastOrDefaultAsync(filter: ac => ac.Receiver == receiver && ac.ExpiryDateTimeUtc > DateTime.UtcNow,
                                        orderBy: ac => ac.Id);

            if (notExpiredAccountCode is not null)
            {
                notExpiredAccountCode.ExpiryDateTimeUtc = DateTime.UtcNow.AddMinutes(-SiteTools.ExpireSmsCodeInMinutes);

                _accountCodeRepository.Update(notExpiredAccountCode);
            }

            var result = await GenerateNewCodeFor(receiver);

            if (result.IsFailure) return Result.Failure<string>(result.Message);

            AccountCode newAccountCode = result.Value;

            await _accountCodeRepository.InsertAsync(newAccountCode);
            await _accountCodeRepository.SaveAsync();

            return newAccountCode.Code;
        }

        public async Task<Result<string>> GetReceiverByCodeAsync(string code)
        {
            var accountCode = await _accountCodeRepository.LastOrDefaultAsync(
                                        filter: c => c.Code == code && c.ExpiryDateTimeUtc > DateTime.UtcNow,
                                        orderBy: c => c.Id
                                    );

            if (accountCode is null) return Result.Failure<string>(ErrorMessages.ConfirmationCodeIsInvalidError);

            return accountCode.Receiver;
        }

        #endregion

        #region Utilities

        protected async Task<Result<AccountCode>> GenerateNewCodeFor(string receiver)
        {
            if (string.IsNullOrWhiteSpace(receiver))
            {
                return Result.Failure<AccountCode>(ErrorMessages.FailedOperationError);
            }

            var isValidMobile = Regex.IsMatch(receiver, @"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$");
            var isValidEmail = Regex.IsMatch(receiver, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!isValidMobile && !isValidEmail)
            {
                return Result.Failure<AccountCode>(ErrorMessages.FormatError);
            }

            var codeLength = isValidEmail ? SiteTools.MailCodeLength : SiteTools.SmsCodeLength;
            var expireInMinutes = isValidEmail ? SiteTools.ExpireMailCodeInMinutes : SiteTools.ExpireSmsCodeInMinutes;
            var accountCode = new AccountCode()
            {
                Code = CommonTools.GenerateRandomNumericCode(codeLength),
                Receiver = receiver,
                ExpiryDateTimeUtc = DateTime.UtcNow.AddMinutes(expireInMinutes)
            };

            return accountCode;
        }

        #endregion
    }
}
