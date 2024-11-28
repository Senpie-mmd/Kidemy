using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Application.ViewModels.Wallet;
using Kidemy.Domain.Models.Wallet;

namespace Kidemy.Application.Mapper
{
    public static class WalletMapper
    {
        public static WalletTransactionDetailsViewModel MapFrom(this WalletTransactionDetailsViewModel mode,
            WalletTransaction wallet)
        {
            mode.Id = wallet.Id;
            mode.Description = wallet.Description;
            mode.CreatedDateOnUtc = wallet.CreatedDateOnUtc;
            mode.Amount = wallet.Amount;
            mode.Balance = wallet.Balance;
            mode.IP = wallet.IP;
            mode.IsSuccess = wallet.IsSuccess;
            mode.RefId = wallet.RefId;
            mode.OrderId = wallet.OrderId;
            mode.TransactionCase = wallet.TransactionCase;
            mode.TransactionType = wallet.TransactionType;
            mode.TransactionWay = wallet.TransactionWay;
            mode.UserId = wallet.UserId;
            mode.PlanId = wallet.PlanId;
            mode.ConsulationRequestId = wallet.ConsulationRequestId;
            return mode;
        }

        public static Expression<Func<WalletTransaction, WalletTransactionViewModel>> MapWalletTransactionViewModel => (WalletTransaction wallet) => new WalletTransactionViewModel
        {
            Id = wallet.Id,
            Description = wallet.Description,
            CreatedDateOnUtc = wallet.CreatedDateOnUtc,
            Amount = wallet.Amount,
            Balance = wallet.Balance,
            IP = wallet.IP,
            IsSuccess = wallet.IsSuccess,
            RefId = wallet.RefId,
            OrderId = wallet.OrderId,
            TransactionCase = wallet.TransactionCase,
            TransactionType = wallet.TransactionType,
            TransactionWay = wallet.TransactionWay,
            UserId = wallet.UserId
        };


    }
}
