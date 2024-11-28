using Kidemy.Application.ViewModels.Newsletter;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Interfaces
{
    public interface INewsletterService
    {
        Task<Result<bool>> CkeckDuplicateEmailAndPhoneNumber(RegisterNewsletterViewModel newsletters);
        Task<Result> RegisterNewsletterMembership(RegisterNewsletterViewModel newsletters);
        Task<Result<FilterNewsletterViewModel>> FilterAsync(FilterNewsletterViewModel model);
        Task<Result> RemoveNewsletterMember(int memberId);
    }
}
