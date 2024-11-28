using Kidemy.Domain.Enums.BankAccount;
using MediatR;

namespace Kidemy.Domain.Events.BankAccountCard
{
    public record BankAccountCardChangeStatusEvent(BankAccountCardStatus lastStatus,
        BankAccountCardStatus currentStatus) : INotification;
}
