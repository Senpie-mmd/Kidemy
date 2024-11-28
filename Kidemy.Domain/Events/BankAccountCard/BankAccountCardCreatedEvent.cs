using Kidemy.Domain.Enums.BankAccount;
using MediatR;

namespace Kidemy.Domain.Events.BankAccountCard
{
    public record BankAccountCardCreatedEvent(
        string bankName,
        string cardNumber,
        string shabaNumber,
        string accountNumber,
        int userId,
        BankAccountCardStatus status,   
        string ownerName
        ) : INotification;

}
