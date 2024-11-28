using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface ISmsSenderSevice
    {
        Task<Result> SendSms(string[] mobiles, string textMessage);
        Task<Result> SendSms(string mobile, string textMessage);
    }
}
