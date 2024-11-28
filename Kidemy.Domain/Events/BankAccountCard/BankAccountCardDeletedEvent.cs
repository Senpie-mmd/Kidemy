using MediatR;

namespace Kidemy.Domain.Events.BankAccountCard
{
    public record BankAccountCardDeletedEvent(
        int id 
        ) : INotification;

}
