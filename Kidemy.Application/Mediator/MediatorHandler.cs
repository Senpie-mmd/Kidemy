using MediatR;

namespace Kidemy.Application.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual Task PublishEvent<T>(T @event) where T : INotification
        {
            return _mediator.Publish(@event);

        }
    }
}
