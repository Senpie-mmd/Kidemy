using Kidemy.Domain.Enums.Discount;
using MediatR;

namespace Kidemy.Domain.Events.Discount
{
    public record DiscountLimitationUpdatedEvent(
        int id,
        int DiscountId,
        DiscountLimitationType PrevType,
        DiscountLimitationType NewType,
        List<int>? PrevUsers,
        List<int>? NewUsers,
        List<int>? PrevCourses,
        List<int>? NewCourses,
        List<int>? PrevCategories,
        List<int>? NewCategories
    ) : INotification;

}
