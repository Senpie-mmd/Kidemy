using Kidemy.Domain.Enums.User;
using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserUpdatedByAdminEvent(
        int Id,
        string? NewEmail,
        string? PrevEmail,
        string NewMobile,
        string PrevMobile,
        string NewFirstName,
        string PrevFirstName,
        string NewLastName,
        string PrevLastName,
        string NewPassword,
        string PrevPassword,
        bool NewIsEmailActive,
        bool PrevIsEmailActive,
        bool NewIsMobileActive,
        bool PrevIsMobileActive,
        bool NewIsBan,
        bool PrevIsBan,
        DateTime? NewBirthDate,
        DateTime? PrevBirthDate,
        Gender NewGender,
        Gender PrevGender,
        List<int>? NewRoleIds,
        List<int>? PrevRoleIds
        ) : INotification;
}
