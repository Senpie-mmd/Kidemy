using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Enums.Wallet;

namespace Kidemy.Domain.Events.Wallet
{
    public record ChargeWalletTransactionCreatedEvent(
        int Id,
        int UserId,
        string IP,
        decimal Amount,
        decimal Balance,
        bool IsSuccess,
        string? Description,
        WalletTransactionType TransactionType,
        WalletTransactionCase TransactionCase,
        WalletTransactionWay TransactionWay
        ) : INotification;
    
}
