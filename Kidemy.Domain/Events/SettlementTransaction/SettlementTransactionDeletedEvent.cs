using MediatR;

namespace Kidemy.Domain.Events.SettlementTransaction
{
    public record SettlementTransactionDeletedEvent(
        int id
        ) : INotification;
}
