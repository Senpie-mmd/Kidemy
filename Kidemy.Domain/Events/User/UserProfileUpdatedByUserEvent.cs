using Kidemy.Domain.Enums.User;
using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserProfileUpdatedByUserEvent(
        int Id,
        string? NewEmail,
        string? PrevEmail,
        string NewFirstName,
        string PrevFirstName,
        string NewLastName,
        string PrevLastName,
        bool NewIsEmailActive,
        bool PrevIsEmailActive,
        DateTime? NewBirthDate,
        DateTime? PrevBirthDate,
        Gender NewGender,
        Gender PrevGender
        ) : INotification;
}
