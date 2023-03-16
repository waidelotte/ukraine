using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.Core.Mediator;

namespace Ukraine.Services.Example.Infrastructure.UseCases;

internal class EventDispatcher : INotificationHandler<EventWrapper>
{
	private readonly IEventBus _eventBus;

	public EventDispatcher(IEventBus eventBus)
	{
		_eventBus = eventBus;
	}

	public async Task Handle(EventWrapper eventWrapper, CancellationToken cancellationToken)
	{
		await _eventBus.PublishAsync(eventWrapper.Event, cancellationToken);
	}
}