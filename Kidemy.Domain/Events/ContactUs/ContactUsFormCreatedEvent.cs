using MediatR;

namespace Kidemy.Domain.Events.ContactUs
{
    public record ContactUsFormCreatedEvent(
        string FullName,
        string Email,
        string Description,
        int UserId

        ) : INotification;
}
