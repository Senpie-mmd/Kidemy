using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Enums.User;
using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserCreatedEvent(
        int Id,
        string? NewEmail,
        string NewMobile,
        string NewFirstName,
        string NewLastName,
        string NewPassword,
        bool NewIsEmailActive,
        bool NewIsMobileActive,
        bool NewIsBan,
        DateTime? NewBirthDate,
        Gender NewGender,
        List<int>? NewRoleIds
        ) : INotification;
}
