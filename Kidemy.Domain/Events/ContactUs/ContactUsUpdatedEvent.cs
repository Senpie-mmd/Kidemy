using MediatR;

namespace Kidemy.Domain.Events.ContactUs
{
    public record ContactUsUpdatedEvent(
        int Id,
        string NewAddress,
        string NewMobile,
        string NewEmail,
        string OldAddress,
        string OldMobile,
        string OldEmail

        ) : INotification;
}
