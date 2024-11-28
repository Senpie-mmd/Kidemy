using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Enums.Wallet;
using MediatR;

namespace Kidemy.Domain.Events.Wallet
{
    public record UndoedWalletTransactionEvent
        (
            int Id,
            int UserId,
            string IP,
            decimal Amount,
            decimal Balance,
            bool IsSuccess,
            WalletTransactionType TransactionType,
            WalletTransactionCase TransactionCase,
            WalletTransactionWay TransactionWay
        ) : INotification;

}
