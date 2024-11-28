using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserActivedEmailEvent
        (
            int Id,
            string? Email
        )
        : INotification;

}
