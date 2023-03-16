using MediatR;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Core.Mediator;

public class EventWrapper : INotification
{
	public EventWrapper(IEvent @event)
	{
		Event = @event;
	}

	public IEvent Event { get; }
}