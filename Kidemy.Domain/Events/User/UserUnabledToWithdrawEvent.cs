using MediatR;

namespace Kidemy.Domain.Events.User
{
    public record UserUnabledToWithdrawEvent(
        int id
        ) : INotification;


}
