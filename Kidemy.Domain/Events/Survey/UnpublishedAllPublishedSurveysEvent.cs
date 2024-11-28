using MediatR;

namespace Kidemy.Domain.Events.Survey
{
    public record UnpublishedAllPublishedSurveysEvent() : INotification;
}
