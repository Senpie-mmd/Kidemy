using MediatR;

namespace Kidemy.Domain.Events.ContactUs
{
    public record ContactUsFormAnsweredEvent(
        int Id
        ) : INotification;
}
