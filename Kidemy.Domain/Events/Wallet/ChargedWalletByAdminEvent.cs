using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Enums.Wallet;
using MediatR;

namespace Kidemy.Domain.Events.Wallet
{
    public record ChargedWalletByAdminEvent(
        int Id,
        int UserId,
        int? OrderId,
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
