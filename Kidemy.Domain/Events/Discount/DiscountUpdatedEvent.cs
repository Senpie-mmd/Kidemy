using Kidemy.Domain.Enums.Discount;
using MediatR;

namespace Kidemy.Domain.Events.Discount
{
    public record DiscountUpdatedEvent(
            int id,
            string PrevTitle,
            string NewTitle,
            string PrevCode,
            string NewCode,
            bool PrevIsActive,
            bool NewIsActive,
            DiscountType PrevType,
            DiscountType NewType,
            bool PrevAutoUse,
            bool NewAutoUse,
            DateTime? PrevStartDateTimeOnUtc,
            DateTime? NewStartDateTimeOnUtc,
            DateTime? PrevEndDateTimeOnUtc,
            DateTime? NewEndDateTimeOnUtc,
            bool PrevIsPercentage,
            bool NewIsPercentage,
            decimal PrevValue,
            decimal NewValue
        ) : INotification;
}
