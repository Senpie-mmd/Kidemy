using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Application.ViewModels.WithrawRequest;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Models.WithdrawRequest;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Kidemy.Application.Mapper
{
    public static class WithdrawRequestMapper
    {
        public static Expression<Func<WithdrawRequest, WithdrawRequestViewModel>> MapAdminSideWithdrawRequestViewModel => (Domain.Models.WithdrawRequest.WithdrawRequest withdrawRequest   ) =>
      new WithdrawRequestViewModel()
      {
          Id = withdrawRequest.Id,
          DestinationBankAccountCardId = withdrawRequest.DestinationBankAccountCardId,
          Description = withdrawRequest.Description,
          Status = withdrawRequest.Status,
          Amount = withdrawRequest.Amount,
          UserId = withdrawRequest.UserId,
          WalletTransactionId = withdrawRequest.WalletTransactionId,
      };

        public static WithdrawRequestViewModel MapFrom(this WithdrawRequestViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.UserName = userInfo.UserName;

            return model;
        }
    }
}
