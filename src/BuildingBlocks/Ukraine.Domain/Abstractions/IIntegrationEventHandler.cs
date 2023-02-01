namespace Ukraine.Domain.Abstractions;

public interface IIntegrationEventHandler { }

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}
