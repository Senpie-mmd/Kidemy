using MediatR;

namespace Kidemy.Domain.Events.SettlementTransaction
{
    public record SettlementTransactionUpdatedEvent(
         decimal lastPrice,
         decimal price,
        string lastCardNumber,
        string cardNumber,
        string lastTrackingNumber,
        string trackingNumber,
        string lastAccountNumber,
        string accountNumber,
        int lastAuserId,
        int userId
        ) : INotification;

}
