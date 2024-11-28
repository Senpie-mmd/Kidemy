using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Enums.Wallet;
using Kidemy.Domain.Shared;
using MediatR;

namespace Kidemy.Domain.Events.Wallet
{
    public record WithdrawedFromWalletForOrderPaymentEvent
        (
            int Id,
            int UserId,
            int? OrderId,
            string IP,
            decimal Amount,
            decimal Balance,
            bool IsSuccess,
            string? RefId,
            string? Description,
            WalletTransactionType TransactionType,
            WalletTransactionCase TransactionCase,
            WalletTransactionWay TransactionWay
        ) : INotification;
}
