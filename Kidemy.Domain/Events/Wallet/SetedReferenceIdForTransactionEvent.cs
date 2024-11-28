using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Kidemy.Domain.Events.Wallet
{
    public record SetedReferenceIdForTransactionEvent
        (
            int Id,
            string? RefId
        ) : INotification;
}
