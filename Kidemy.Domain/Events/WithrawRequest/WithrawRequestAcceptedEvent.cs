using Kidemy.Domain.Models.WithdrawRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.WithrawRequest
{
    public record WithrawRequestAcceptedEvent(
        WithdrawRequestStatus status,
        string description,
        string refId
        ) :INotification;
    
    
}
