using MediatR;

namespace Kidemy.Domain.Events.ContactUs
{
    public record ContactUsFormDeletedEvent(
        int Id
        ) : INotification;
}
