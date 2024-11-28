using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.SettlementTransaction
{
    public record SettlementTransactionCreatedEvent(
        decimal price,
        string cardNumber,
        string trackingNumber,
        string accountNumber,
        int userId
        ):INotification;
    
    
}
