using MediatR;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Core.Mediator;

public static class PublisherExtensions
{
	public static async Task PublishEvents(
		this IPublisher publisher,
		IAggregateRoot aggregateRoot,
		CancellationToken cancellationToken = default)
	{
		if (aggregateRoot.DomainEvents is not null)
		{
			var events = new IEvent[aggregateRoot.DomainEvents.Count];
			aggregateRoot.DomainEvents.CopyTo(events);
			aggregateRoot.DomainEvents.Clear();

			foreach (var @event in events)
			{
				await publisher.Publish(new EventWrapper(@event), cancellationToken);
			}
		}
	}
}