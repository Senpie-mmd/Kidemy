using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserabledToWithdrawEvent(
        int id) : INotification;
}
