using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Models.BankAccountCard;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class BankAccountCardMapper
    {
        public static Expression<Func<BankAccountCard, BankAccountCardViewModel>> MapAdminSideBankAccountCardsViewModel => (BankAccountCard bankAccountCard) =>
     new BankAccountCardViewModel()
     {
         Id = bankAccountCard.Id,
         ShabaNumber = bankAccountCard.ShabaNumber,
         Status = bankAccountCard.Status,
         AccountNumber = bankAccountCard.AccountNumber,
         BankName = bankAccountCard.BankName,
         CardNumber = bankAccountCard.CardNumber,
         Description = bankAccountCard.Description,
         IsDefault = bankAccountCard.IsDefault,
         UserId = bankAccountCard.UserId,
         OwnerName= bankAccountCard.OwnerName,
     };


        public static BankAccountCardViewModel MapFrom(this BankAccountCardViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.UserName = userInfo.UserName;

            return model;
        }

        public static BankAccountCardViewModel MapFrom(this BankAccountCardViewModel model, BankAccountCard bankAccountCard)
        {
            model.Id = bankAccountCard.Id;
            model.CardNumber = bankAccountCard.CardNumber;
            model.AccountNumber = bankAccountCard.AccountNumber;
            model.ShabaNumber = bankAccountCard.ShabaNumber;
            model.Status = bankAccountCard.Status;
            model.IsDefault = bankAccountCard.IsDefault;
            model.Description = bankAccountCard.Description;
            model.UserId = bankAccountCard.UserId;
            model.OwnerName= bankAccountCard.OwnerName;
            return model;
        }
    }
}
