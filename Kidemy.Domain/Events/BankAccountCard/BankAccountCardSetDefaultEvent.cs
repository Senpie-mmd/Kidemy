using MediatR;

namespace Kidemy.Domain.Events.BankAccountCard
{
    public record BankAccountCardSetDefaultEvent(
        int? lastDefaultAccountId,
        int currentDefaultAccountId
        ) : INotification;

}
