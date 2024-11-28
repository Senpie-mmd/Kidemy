using Kidemy.Application.Convertors;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.SettlementTransaction;
using Kidemy.Domain.Models.SettlementTransaction;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class SettlementTransactionMapper
    {
        public static Expression<Func<SettlementTransaction, AdminSideSettlementTransactionViewModel>> MapAdminSideSettlementTransactionViewModel => (SettlementTransaction settlementTransaction) =>
      new AdminSideSettlementTransactionViewModel()
      {
          Id = settlementTransaction.Id,
          TransactionDate = settlementTransaction.TransactionDate,
          Price = settlementTransaction.Price,
          CardNumber = settlementTransaction.CardNumber,
          AccountNumber = settlementTransaction.AccountNumber,
          TrackingCode = settlementTransaction.TrackingCode,
          UserId = settlementTransaction.UserId,
          IsDelete = settlementTransaction.IsDelete,
          CreateDateUTC = settlementTransaction.CreatedDateOnUtc,
          UpdateDateUTC = settlementTransaction.UpdatedDateOnUtc
      };
        public static AdminSideSettlementTransactionViewModel MapFrom(this AdminSideSettlementTransactionViewModel model, SettlementTransaction settlementTransaction)
        {

            model.Id = settlementTransaction.Id;
            model.Price = settlementTransaction.Price;
            model.UserId = settlementTransaction.UserId;
            model.IsDelete = settlementTransaction.IsDelete;
            model.AccountNumber = settlementTransaction.AccountNumber;
            model.CardNumber = settlementTransaction.CardNumber;
            model.CreateDateUTC = settlementTransaction.CreatedDateOnUtc;
            model.TransactionDate = settlementTransaction.TransactionDate;
            model.UpdateDateUTC = settlementTransaction.UpdatedDateOnUtc;
            return model;

        }

        public static AdminSideUpsertSettlementTransactionViewModel MapFrom(this AdminSideUpsertSettlementTransactionViewModel model, SettlementTransaction settlementTransaction)
        {

            model.Id = settlementTransaction.Id;
            model.Price = (long)settlementTransaction.Price;
            model.UserId = settlementTransaction.UserId;
            model.AccountNumber = settlementTransaction.AccountNumber;
            model.CardNumber = settlementTransaction.CardNumber;
            model.TrackingCode = settlementTransaction.TrackingCode;
            model.TransactionDate = settlementTransaction.TransactionDate.ToUserLongDateTime();
            return model;

        }
        public static SettlementTransaction MapFrom(this SettlementTransaction settlementTransaction, AdminSideUpsertSettlementTransactionViewModel model)
        {
            settlementTransaction.AccountNumber = model.AccountNumber;
            settlementTransaction.CardNumber = model.CardNumber;
            settlementTransaction.TrackingCode = model.TrackingCode;
            settlementTransaction.TransactionDate = model.TransactionDate.ConvertToEnglishNumber().ParseUserDateToUTC().Value;
            settlementTransaction.Price = (decimal)model.Price;
            settlementTransaction.UpdatedDateOnUtc = DateTime.UtcNow;

            return settlementTransaction;
        }

        public static Expression<Func<SettlementTransaction, ClientSideSettlementTransactionViewModel>> MapClientSideSettlementTransactionViewModel => (SettlementTransaction settlementTransaction) =>
     new ClientSideSettlementTransactionViewModel()
     {
         Id = settlementTransaction.Id,
         TransactionDate = settlementTransaction.TransactionDate,
         Price = settlementTransaction.Price,
         CardNumber = settlementTransaction.CardNumber,
         AccountNumber = settlementTransaction.AccountNumber,
         TrackingCode = settlementTransaction.TrackingCode,
         UserId = settlementTransaction.UserId,
         IsDelete = settlementTransaction.IsDelete,
         CreateDateUTC = settlementTransaction.CreatedDateOnUtc,
         UpdateDateUTC = settlementTransaction.UpdatedDateOnUtc
     };

    }
}
