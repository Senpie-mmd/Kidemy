using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface IAccountCodeService
    {
        Task<Result<string>> GenerateAccountCodeAsync(string receiver);
        Task<Result<string>> GetReceiverByCodeAsync(string code);
        Task<Result> ValidateCodeAsync(string receiver, string code);
    }
}
